using Projekt_ASP.Data;
using Projekt_ASP.DTO;

namespace Projekt_ASP.PasswordOptions
{
    public static class PasswordChecker
    {
        public static async Task varunkiZmianyHasla(User user, ChangePassword change)
        {


            if (change.NewPassword.Length >= 8)
            {
                for (int i = 0; i < change.NewPassword.Length; i++)
                {
                    var ifTrue = Char.IsUpper(change.NewPassword, i);
                    if (ifTrue)
                    {

                        for (int y = 0; y < change.NewPassword.Length; y++)
                        {
                            var ifTrueSym4 = Char.IsPunctuation(change.NewPassword, y);

                            if (ifTrueSym4)
                            {

                                
                                await Task.CompletedTask;
                                break;


                            }
                            else if (y == change.NewPassword.Length - 1)
                            {
                                throw new Exception("Nie spełniono wymagan hasla");
                            }

                        }
                        break;

                    }
                    else if (i == change.NewPassword.Length - 1)
                    {
                        throw new Exception("Nie spełniono wymagan hasla");
                    }

                }

            }
            else if (user.Password.Length < 8)
            {
                throw new Exception("Nie spełniono wymagan hasla");
            }
        }











        public static async Task poprzednieHasla(ChangePassword change, OldPassword oldPassword)
        {
            foreach (var item in oldPassword.Passwords)
            {
                if (item == change.NewPassword)
                {
                    throw new Exception("Haslo sie powtarza");
                }
            }
            await Task.CompletedTask;
        }
    }
}
