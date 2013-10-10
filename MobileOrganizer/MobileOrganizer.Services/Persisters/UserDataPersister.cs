using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MobileOrganizer.Services.Persisters
{
    public class UserDataPersister
    {
        private static readonly Random rand = new Random();
        private const int MinUsernameLength = 6;
        private const int MaxUsernameLength = 30;

        private const int MinNicknameLength = 6;
        private const int MaxNicknameLength = 30;

        private const int Sha1Length = 40;
        private const int SessionKeyLength = 50;

        private const string ValidUsernameCharacters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890._";
        private const string ValidNicknameCharacters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890._ -";
        private const string ValidSha1Characters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";
        private const string ValidSessionKeyCharacters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";


        public static string GenerateSessionKey(int userId)
        {
            StringBuilder sessionKey = new StringBuilder();
            sessionKey.Append(userId);

            while (sessionKey.Length < SessionKeyLength)
            {
                int index = rand.Next(ValidSessionKeyCharacters.Length);
                sessionKey.Append(ValidSessionKeyCharacters[index]);
            }

            return sessionKey.ToString();
        }

        public static void ValidateAuthCode(string authCode)
        {
            if (authCode == null)
            {
                throw new ArgumentNullException("AuthCode cannot be null");
            }
            else if (authCode.Length < Sha1Length)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("AuthCode must be exact {0} characters long", Sha1Length));
            }
            else if (authCode.Any(ch => !ValidSha1Characters.Contains(ch)))
            {
                throw new ArgumentOutOfRangeException("AuthCode must contains Latin letters and digits");
            }
        }

        public static void ValidateNickname(string nickname)
        {
            if (nickname == null)
            {
                throw new ArgumentNullException("Nickname cannot be null");
            }
            else if (nickname.Length < MinNicknameLength)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Nickname must be more than {0} characters long", MinNicknameLength));
            }
            else if (nickname.Length > MaxNicknameLength)
            {
                throw new ArgumentOutOfRangeException(
                 string.Format("Nickname must be less than {0} characters long", MaxNicknameLength));
            }
            else if (nickname.Any(ch => !ValidNicknameCharacters.Contains(ch)))
            {
                throw new ArgumentOutOfRangeException("Nickname must contains Latin letters, digits, ., and _");
            }
        }

        public static void ValidateUsername(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException("Username cannot be null");
            }
            else if (username.Length < MinUsernameLength)
            {
                throw new ArgumentOutOfRangeException(
                    string.Format("Username must be more than {0} characters long", MinUsernameLength));
            }
            else if (username.Length > MaxUsernameLength)
            {
                throw new ArgumentOutOfRangeException(
                 string.Format("Username must be less than {0} characters long", MaxUsernameLength));
            }
            else if (username.Any(ch => !ValidUsernameCharacters.Contains(ch)))
            {
                throw new ArgumentOutOfRangeException("Username must contains Latin letters, digits, ., and _");
            }
        }
    }
}