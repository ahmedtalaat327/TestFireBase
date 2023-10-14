using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace FireBaseAsDModel
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
        /// Make my custom configs
        /// </summary>
        /// <param name="secretKey">secet key captured by FB sys</param>
        /// <param name="urlPath">url hash</param>
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
                return MakeConnectionConfigs(secretKey,urlPath);
            }
            else
            {
                return _iconfig;
            }
        }
        /// <summary>
        /// connect to secure db using configuration already set
        /// </summary>
        /// <param name="firebaseConfig">fb configuration</param>
        /// <returns></returns>
        public int ConnectToServer(IFirebaseConfig firebaseConfig)
        {


            if (firebaseConfig == null)
                return -1;
            else
            {
                FirebaseClient cl = null;
                try
                {
                    cl = new FirebaseClient(firebaseConfig);
                    return 0;

                }
                catch (Exception er)
                {
                    Console.WriteLine(er.Message);
                    return -1;
                }
            }
                 
        }
        #endregion
    }
}
