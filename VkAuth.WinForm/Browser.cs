using System;
using System.Net;

namespace VkAuth.WinForm
{
    /// <summary>
    /// Браузер, через который производится сетевое взаимодействие с ВКонтакте.
    /// Сетевое взаимодействие выполняется с помощью HttpWebRequest
    /// </summary>
    public class Browser
    {
        /// <summary>
        /// Прокси сервер
        /// </summary>
        public IWebProxy Proxy { get; set; }

        /// <summary>
        /// Авторизация на сервере ВК
        /// </summary>
        /// <param name="email">Логин - телефон или эл. почта</param>
        /// <param name="password">Пароль</param>
        /// <param name="request">request</param>
        /// <param name="code">Код двухфакторной авторизации</param>
        /// <param name="captchaSid">Идентификатор капчи</param>
        /// <param name="captchaKey">Текст капчи</param>
        /// <returns>Информация об авторизации приложения</returns>
        public Uri Authorize(string email, string password, Uri uri, Func<string> code = null, long? captchaSid = null, string captchaKey = null)
        {
            // 1. Открытие диалога авторизации
            var authorizeUrlResult = OpenAuthDialog(uri);

            if (IsAuthSuccessfull(authorizeUrlResult))
            {
                return EndAuthorize(authorizeUrlResult, Proxy);
            }

            // 2. Заполнение формы логина
            var loginFormPostResult = FillLoginForm(email, password, captchaSid, captchaKey, authorizeUrlResult);

            if (IsAuthSuccessfull(loginFormPostResult))
            {
                return EndAuthorize(loginFormPostResult, Proxy);
            }

            // 2.5.1. Заполнить код двухфакторной авторизации
            if (HasNotTwoFactor(code, loginFormPostResult))
            {
                return EndAuthorize(loginFormPostResult, Proxy);
            }

            var twoFactorFormResult = FilledTwoFactorForm(code, loginFormPostResult);
            if (IsAuthSuccessfull(twoFactorFormResult))
            {
                return EndAuthorize(twoFactorFormResult, Proxy);
            }

            // 2.5.2 Капча
            var captchaForm = WebForm.Create(twoFactorFormResult);

            var captcha = WebCall.Post(captchaForm, Proxy);
            // todo: Нужно обработать капчу

            return EndAuthorize(captcha, Proxy);
        }

        /// <summary>
        /// Заполнить форму двухфакторной авторизации
        /// </summary>
        /// <param name="code">Функция возвращающая код двухфакторной авторизации</param>
        /// <param name="loginFormPostResult">Ответ сервера vk</param>
        /// <returns>Ответ сервера vk</returns>
        private WebCallResult FilledTwoFactorForm(Func<string> code, WebCallResult loginFormPostResult)
        {
            var codeForm = WebForm.Create(loginFormPostResult)
                .WithField("code")
                .FilledWith(code.Invoke());

            return WebCall.Post(codeForm, Proxy);
        }

        /// <summary>
        /// Проверка наличия двухфакторной авторизации
        /// </summary>
        /// <param name="code">Функция возвращающая код двухфакторной авторизации</param>
        /// <param name="loginFormPostResult">Ответ сервера vk</param>
        /// <returns></returns>
        private static bool HasNotTwoFactor(Func<string> code, WebCallResult loginFormPostResult)
        {
            return code == null || WebForm.IsOAuthBlank(loginFormPostResult);
        }

        /// <summary>
        /// Заполнить форму логин и пароль
        /// </summary>
        /// <param name="email">Логин</param>
        /// <param name="password">Пароль</param>
        /// <param name="captchaSid">ИД капчи</param>
        /// <param name="captchaKey">Значение капчи</param>
        /// <param name="authorizeUrlResult"></param>
        /// <returns></returns>
        private WebCallResult FillLoginForm(string email, string password, long? captchaSid, string captchaKey, WebCallResult authorizeUrlResult)
        {
            var loginForm = WebForm.Create(authorizeUrlResult)
                .WithField("email")
                .FilledWith(email)
                .And()
                .WithField("pass")
                .FilledWith(password);

            if (captchaSid.HasValue)
            {
                loginForm.WithField("captcha_sid")
                    .FilledWith(captchaSid.Value.ToString())
                    .WithField("captcha_key")
                    .FilledWith(captchaKey);
            }

            return WebCall.Post(loginForm, Proxy);
        }

        /// <summary>
        /// Выполняет обход ошибки валидации: https://vk.com/dev/need_validation
        /// </summary>
        /// <param name="validateUrl">Адрес страницы валидации</param>
        /// <param name="phoneNumber">Номер телефона, который необходимо ввести на странице валидации</param>
        /// <returns>Информация об авторизации приложения.</returns>
        public Uri Validate(string validateUrl, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(validateUrl))
            {
                throw new ArgumentException("Не задан адрес валидации!");
            }

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentException("Не задан номер телефона!");
            }

            var validateUrlResult = WebCall.MakeCall(validateUrl, Proxy);
            var codeForm = WebForm.Create(validateUrlResult)
                .WithField("code")
                .FilledWith(phoneNumber.Substring(1, 8));
            var codeFormPostResult = WebCall.Post(codeForm, Proxy);

            return EndAuthorize(codeFormPostResult, Proxy);
        }

        /// <summary>
        /// Закончить авторизацию
        /// </summary>
        /// <param name="result">Результат</param>
        /// <param name="webProxy">Настройки прокси</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private Uri EndAuthorize(WebCallResult result, IWebProxy webProxy = null)
        {
            var tokenUri = GetTokenUri(result);
            var authorization = VkUriParseResult.Create(tokenUri);

            if (!authorization.IsAuthorizationRequired && !authorization.IsCaptchaNeeded)
            {
                return tokenUri;
            }

            // Отправить данные
            var authorizationForm = WebForm.Create(result);
            var authorizationFormPostResult = WebCall.Post(authorizationForm, webProxy);
            return GetTokenUri(authorizationFormPostResult); 
        }


        /// <summary>
        /// Открытие окна авторизации  
        /// </summary>
        /// <param name="Request">Request</param>
        /// <returns></returns>
        /// TODO DisplayPageType = Page! it was before in this shit
        private WebCallResult OpenAuthDialog(Uri uri)
        {
            return WebCall.MakeCall(uri.AbsoluteUri, Proxy);
        }

        /// <summary>
        /// Авторизация прошла успешно
        /// </summary>
        /// <param name="webCallResult"></param>
        /// <returns>true, если авторизация прошла успешно</returns>
        private static bool IsAuthSuccessfull(WebCallResult webCallResult) => UriHasAccessToken(webCallResult.RequestUrl) || UriHasAccessToken(webCallResult.ResponseUrl);

        /// <summary>
        /// Проверка наличия токена в url
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        private static bool UriHasAccessToken(Uri uri) => uri.Fragment.StartsWith("#access_token=", StringComparison.Ordinal);

        /// <summary>
        /// Получить токен из uri
        /// </summary>
        /// <param name="webCallResult">Результат запроса</param>
        /// <returns>Возвращает uri содержащий токен</returns>
        /// <exception cref="VkApiException">URI должен содержать токен!</exception>
        private static Uri GetTokenUri(WebCallResult webCallResult)
        {
            if (UriHasAccessToken(webCallResult.RequestUrl))
            {
                return webCallResult.RequestUrl;
            }

            if (UriHasAccessToken(webCallResult.ResponseUrl))
            {
                return webCallResult.ResponseUrl;
            }

            throw new Exception("URI должен содержать токен!");
        }
    }
}