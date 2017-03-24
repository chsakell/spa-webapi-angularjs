using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Entities.Settings
{
    // classe de paramétrage hérite de ISettings et manupilable avec SetttingService
    public class ServerSettings : ISettings
    {
        public ServerSettings()
        {
        }
        /// <summary>
        /// Gets or sets a value indicating "sqlServer master Admin account name"
        /// </summary>
        public string sqlServerAdmin { get; set; }
        /// <summary>
        /// Gets or sets a value indicating "sqlServer master Admin password"
        /// </summary>
        public string sqlServerPassword { get; set; }
        /// <summary>
        /// Gets or sets a value indicating "api access token expire in"
        /// </summary>
        public int accessTokenExpireTimeSpan { get; set; }
        /// <summary>
        /// Gets or sets a value indicating "mail Admin account name"
        /// </summary>
        public string mailAdmin { get; set; }
        /// <summary>
        /// Gets or sets a value indicating "mail Admin account server"
        /// </summary>
        public string mailPassword { get; set; }
        /// <summary>
        /// Gets or sets a value indicating "mail  account password"
        /// </summary>
        public string smtpServer { get; set; }
        /// <summary>
        /// Gets or sets a value indicating "mail SMTP account Port"
        /// </summary>
        public string smtpPort { get; set; }
        /// <summary>
        /// Gets or sets a value indicating "user account password required length"
        /// </summary>
        public int RequiredLength { get; set; }
        /// <summary>
        /// Gets or sets a value indicating "UserLockoutEnabledByDefault"
        /// </summary>
        public bool UserLockoutEnabledByDefault { get; set; }
        /// <summary>
        /// Gets or sets a value indicating "DefaultAccountLockoutTimeSpan"
        /// </summary>
        public TimeSpan DefaultAccountLockoutTimeSpan { get; set; }
        /// <summary>
        /// Gets or sets a value indicating "MaxFailedAccessAttemptsBeforeLockout"
        /// </summary>
        public int MaxFailedAccessAttemptsBeforeLockout { get; set; }
        /// <summary>
        /// Gets or sets a value indicating "Niveau Log"
        /// </summary>
        public string LogLevel { get; set; }
    }
}
