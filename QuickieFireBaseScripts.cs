using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;

namespace FireBaseAsDModel
{
    public class QuickieFireBaseScripts
    {
        #region Members 
        FirebaseClient firebaseClient = null;
        public FirebaseConfig firebaseConfigs = null;
        #endregion

        #region ctor 
        /// <summary>
        /// Be aware we are trying to avoid to call any firebase object outside this DLL
        /// initialize current class object with 2 strings
        /// </summary>
        /// <param name="currentDBListSecretCode">get this serial from your google acc</param>
        /// <param name="path">hash to db</param>
        public QuickieFireBaseScripts(string currentDBListSecretCode, string path) {
            //call this async methods with error maybe called
            firebaseConfigs = MakeConnectionConfigs(currentDBListSecretCode, path);   
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
        /// <returns>any int number greater than -1 if successfull</returns>
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
                    firebaseClient = cl;
                    return 0;

                }
                catch (Exception er)
                {
                    Console.WriteLine(er.Message);
                    return -1;
                }
            }
                 
        }
        /// <summary>
        /// trying too gt the max no of list
        /// </summary>
        /// <typeparam name="T">Data Type of these lists</typeparam>
        /// <param name="dblistName">current list name inside firebaase db cloud</param>
        /// <returns>max count if fails will be zero!</returns>
        public int GetMaxID<T>(string dblistName)
        {
            try
            {
                var getter = firebaseClient.Get(dblistName + "/");

                var result = getter.ResultAs<List<T>>();

                return result.Count - 1;
            }
            catch (Exception er)
            {
                return 0;

            }
        }
        /// <summary>
        /// pushing new node to tree
        /// </summary>
        /// <typeparam name="T">type of object</typeparam>
        /// <param name="dblistName">name of list in cloud </param>
        /// <param name="nextid">calcualted next id for current node will be added</param>
        /// <param name="nodeObj">node object</param>
        /// <returns></returns>
        public string PushNewNode<T>(string dblistName, int nextid, T nodeObj)
        {
            

            try
            {
                var setter = firebaseClient.Set<T>(dblistName + "/" + nextid, nodeObj);

                return setter.StatusCode.ToString();
            }
            catch (Exception er)
            {
                return "-1";

            }



        }
        /// <summary>
        /// getting data as object from current node
        /// </summary>
        /// <typeparam name="T">datatype you actually used in db cloud system as node</typeparam>
        /// <param name="dblistName">name of list</param>
        /// <param name="currentlyid">key to be used as integer number</param>
        /// <returns></returns>
        public T PullNodeAsObject<T>(string dblistName,int currentlyid) where T: new() {

            return new T();
        }
        #endregion
    }
}
