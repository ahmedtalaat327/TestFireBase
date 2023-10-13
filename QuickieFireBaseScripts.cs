using FireSharp.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFireBase
{
    public class QuickieFireBaseScripts
    {

        #region ctor 
        /// <summary>
        /// Be aware we are trying to avoid to call any firebase object outside this DLL
        /// initialize current class object with 2 strings
        /// </summary>
        /// <param name="currentDBListSecretCode">get this serial from your google acc</param>
        /// <param name="path">hash to db</param>
        public QuickieFireBaseScripts(string currentDBListSecretCode, string path) {
            //call this async methods with error maybe called
            MakeConnectionConfigs(currentDBListSecretCode, path);   
        }
        #endregion
        #region Helpers Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="urlPath"></param>
        /// <returns></returns>
        private FirebaseConfig MakeConnectionConfigs(string secretKey,string urlPath)
        {

            FirebaseConfig _iconfig = new FirebaseConfig()
            {

                AuthSecret = secretKey,
                BasePath = urlPath,

            };

            if(_iconfig.RequestTimeout>new TimeSpan(5000))
            {
                return new FirebaseConfig() { };
            }
            else
            {
                return _iconfig;
            }
        }
        #endregion
    }
}
