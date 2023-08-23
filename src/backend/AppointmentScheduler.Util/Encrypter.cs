using System.Security.Cryptography;
using System.Text;

namespace AppointmentScheduler.Util;

public static class Encrypter
{
    public static string ToSha256(string text)
    {
        ASCIIEncoding encoding = new();
        StringBuilder stringBuilder = new();
        byte[] stream = SHA256.HashData(encoding.GetBytes(text));
        for (int i = 0; i < stream.Length; i++)
            stringBuilder.AppendFormat("{0:x2}", stream[i]);

        return stringBuilder.ToString();
    }
}
