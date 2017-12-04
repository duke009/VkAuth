using System;

namespace VkAuth.WinForm
{
    /// <summary>
    /// Информация об авторизации приложения на действия.
    /// </summary>
    public class VkUriParseResult
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="uri">URL ответа.</param>
        private VkUriParseResult(Uri uri)
        {
            if (string.IsNullOrWhiteSpace(uri.Query) && string.IsNullOrWhiteSpace(uri.Fragment))
                return;

            var nameValue = VkAuthHelper.ParseUri(uri);

            IsCaptchaNeeded = !string.IsNullOrEmpty(nameValue["sid"]);
            IsAuthorizationRequired = !string.IsNullOrEmpty(nameValue["__q_hash"]);
        }

        /// <summary>
        /// Извлекает из URL, на которую произошло перенаправление при авторизации, информацию об авторизации.
        /// </summary>
        /// <param name="uri">
        /// URL, на которую произошло перенаправление при авторизации.
        /// </param>
        /// <returns>Информация об авторизации.</returns>
        public static VkUriParseResult Create(Uri uri) => new VkUriParseResult(uri);

        /// <summary>
        /// Проверяет требуется ли получения у авторизации на запрошенные приложением действия (при установке приложения пользователю).
        /// </summary>
        public bool IsAuthorizationRequired { get; }

        /// <summary>
        /// ID капчи, если она появилась
        /// </summary>
        public bool IsCaptchaNeeded { get; }
    }
}