﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace FireBaseAsDModel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            FirebaseConfig iconfigB = new FirebaseConfig() { 
            
            AuthSecret = "Wj2pfAtfyrGWtIC7kW7fp33Z5AISS7A5r179XZhV",
            BasePath = "https://mriuserprofile-default-rtdb.firebaseio.com/"

            };

            MakeConnectionAuth("MyUsers",iconfigB);

            Console.ReadLine();
            */


            QuickieFireBaseScripts quickieFireBaseScripts = new QuickieFireBaseScripts(
                "Wj2pfAtfyrGWtIC7kW7fp33Z5AISS7A5r179XZhV",
                "https://mriuserprofile-default-rtdb.firebaseio.com/"
                );


            if (quickieFireBaseScripts.ConnectToServer(quickieFireBaseScripts.firebaseConfigs) > -1)
                Console.WriteLine("Connected to Firebase successfully");
            else
                Console.WriteLine("Connecting to Firebase failed!");


            //caluculated max id
            var index = quickieFireBaseScripts.GetMaxID<User>("MyUsers");
            Console.WriteLine("Next Id is: "+(index+1));



            //create object
            User usr_obj = new User() {
                Id = index + 1, Name = "Ahmed Talaat Mohamed", Email = "a.talaat@jooo.com", Password = "qh199"
            };
            Console.WriteLine("trying to add new user ...");

            if(quickieFireBaseScripts.PushNewNode<User>("MyUsers", usr_obj.Id, usr_obj)=="-1")
                Console.WriteLine("User successfuly added to database cloud system.");
            else
            {
                Console.WriteLine("Failed to add anything");
                return;
            }
            Console.ReadLine();
        }
        private static void MakeConnectionAuth(string dblistName, IFirebaseConfig firebaseConfig)
        {
            if (firebaseConfig == null)
                return;

            FirebaseClient cl = null;
            try
            {
                 cl = new FirebaseClient(firebaseConfig);
                   
            }
            catch(Exception er) {
                Console.WriteLine(er.Message);
                return;
            }


            //if proceed
            //first calculate the next ID to be added for.
            //
            var iD_next = GetMaxID(cl, $"{dblistName}")+1;
            Console.WriteLine("next user id " + iD_next);


            if (CreateUser(cl, $"{dblistName}",iD_next) == "-1")
                Console.WriteLine("error not added");
            else Console.WriteLine("added ^^");

            
           

        }
        private static string CreateUser(FirebaseClient client,string dblistName, int nextid)
        {
            User us1 = new User() { 
            Id = nextid, Email="ppxxxxxxx.com",Name="xxxxxxxx",Password = "tttttttt"
            };

            try
            {
                var setter = client.Set<User>(dblistName + "/" + us1.Id, us1);
                
                return setter.StatusCode.ToString();
            }
            catch(Exception er) {
                return "-1";

            }
            

            
        }
        private static int GetMaxID(FirebaseClient client, string dblistName)
        {
            try
            {
                var getter = client.Get(dblistName + "/");

                var result = getter.ResultAs<List<User>>();

                return result.Count-1;
            }
            catch (Exception er)
            {
                return 0;

            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Email { get; set; }   
     
        public string Password { get; set; }    

    }
}
