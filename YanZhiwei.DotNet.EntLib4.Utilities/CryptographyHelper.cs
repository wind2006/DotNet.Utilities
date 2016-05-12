using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;

namespace YanZhiwei.DotNet.EntLib4.Utilities
{
    public class CryptographyHelper
    {
        /*
         *知识：
         *1. 产生 key 文件的过程中有个选择项， 一个是用户模式，一个是机器模式。
         *   用户模式是登陆到该计算机的用户才能使用这个 key文件，机器模式当然就是只要是这个计算机上的用户都可以使用这个 key 文件。比如部署 WebForms 的WebSite 需要使用机器模式，至于WinForms的部署，如果不能确认计算机上只有一个帐户会使用你部署的软件，还是使用机器模式；
         *2. 对称加密(也叫私钥加密)指加密和解密使用相同密钥的加密算法。有时又叫传统密码算法，就是加密密钥能够从解密密钥中推算出来，同时解密密钥也可以从加密密钥中推算出来。而在大多数的对称算法中，加密密钥和解密密钥是相同的，所以也称这种加密算法为秘密密钥算法或单密钥算法。它要求发送方和接收方在安全通信之前，商定一个密钥。对称算法的安全性依赖于密钥，泄漏密钥就意味着任何人都可以对他们发送或接收的消息解密，所以密钥的保密性对通信性至关重要。 
         *
         *3.对称加密算法的特点是算法公开、计算量小、加密速度快、加密效率高。
         *
         *4.不足之处是，交易双方都使用同样钥匙，安全性得不到保证。此外，每对用户每次使用对称加密算法时，都需要使用其他人不知道的惟一钥匙，这会使得发收信双方所拥有的钥匙数量呈几何级数增长，密钥管理成为用户的负担。对称加密算法在分布式网络系统上使用较为困难，主要是因为密钥管理困难，使用成本较高。而与公开密钥加密算法比起来，对称加密算法能够提供加密和认证却缺乏了签名功能，使得使用范围有所缩小。在计算机专网系统中广泛使用的对称加密算法有DES和IDEA等。美国国家标准局倡导的AES即将作为新标准取代DES。 
         *
         *5.对称加密具体算法：DES算法，3DES算法，TDEA算法，Blowfish算法，RC5算法，IDEA算法。
         *
         * 
         */

        /// <summary>
        /// 对称加密
        /// </summary>
        /// <param name="symmetricName">Name of the symmetric.</param>
        /// <param name="encrytData">The encryt data.</param>
        /// <returns></returns>
        public static string EncryptSymmetric(string symmetricName, string encrytData)
        {
            return Cryptographer.EncryptSymmetric(symmetricName, encrytData);
        }

        /// <summary>
        /// 对称解密
        /// </summary>
        /// <param name="symmetricName">Name of the symmetric.</param>
        /// <param name="decryptData">The decrypt data.</param>
        /// <returns></returns>
        public static string DecryptSymmetric(string symmetricName, string decryptData)
        {
            return Cryptographer.DecryptSymmetric(symmetricName, decryptData);
        }

        /// <summary>
        /// Creates the hash.
        /// </summary>
        /// <param name="hashName">Name of the hash.</param>
        /// <param name="encrytData">The encryt data.</param>
        /// <returns></returns>
        public static string CreateHash(string hashName, string encrytData)
        {
            return Cryptographer.CreateHash(hashName, encrytData);
        }

        /// <summary>
        /// Compares the hash.
        /// </summary>
        /// <param name="hashName">Name of the hash.</param>
        /// <param name="decryptData">The decrypt data.</param>
        /// <param name="encrytData">The encryt data.</param>
        /// <returns></returns>
        public static bool CompareHash(string hashName, string decryptData, string encrytData)
        {
            return Cryptographer.CompareHash(hashName, decryptData, encrytData);
        }
    }
}