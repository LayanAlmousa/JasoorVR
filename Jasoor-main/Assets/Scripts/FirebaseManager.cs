using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using System;
using System.Threading.Tasks; 
using Firebase.Extensions;

public class FirebaseManager : MonoBehaviour
{
public GameObject loginPanel, forgotPasswordPanel; //signupPanel, profilePanel;

public InputField loginEmail, loginPassword, forgotPassEmail; //signupEmail, signupPassword, signupUsername, signupCPassword;

Firebase.Auth.FirebaseAuth auth;
Firebase.Auth.FirebaseUser user;

bool isSignIn = false;

void Start()
{

Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
  var dependencyStatus = task.Result;
  if (dependencyStatus == Firebase.DependencyStatus.Available) {
    // Create and hold a reference to your FirebaseApp,
    // where app is a Firebase.FirebaseApp property of your application class.
       InitializeFirebase();
 
    // Set a flag here to indicate whether Firebase is ready to use by your app.
  } else {
    UnityEngine.Debug.LogError(System.String.Format(
      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
    // Firebase Unity SDK is not safe to use here.
  }
});

}

public void OpenLoginPanel() {
    loginPanel.SetActive(true);
    // signupPanel.SetActive(false);
    // profilePanel.SetActive(false); 
    forgotPasswordPanel.SetActive(false);
}

// if we plan on having a signup page: 

// public void OpenSignupPanel() {
//     loginPanel.SetActive(false);
//     // signupPanel.SetActive(true);
//     // profilePanel.SetActive(false);
//     forgotPasswordPanel.SetActive(false);
// }

// if we plan on having a profile page:

// public void OpenProfilePanel() {
//     loginPanel.SetActive(false);
//     // signupPanel.SetActive(false);
//     // profilePanel.SetActive(true);
//     forgotPasswordPanel.SetActive(false);
// }

public void OpenForgotPassPanel() {
    loginPanel.SetActive(false);
    // signupPanel.SetActive(false);
    // profilePanel.SetActive(flase);
    forgotPasswordPanel.SetActive(true);
}


public void LoginUser() {
    if (string.IsNullOrEmpty(loginEmail.text)&&string.IsNullOrEmpty(loginPassword.text)) {

        return;
       
    }
      // Do Login
      SignInUser(loginEmail.text , loginPassword.text); 
}

public void ForgotPass() {
    if (string.IsNullOrEmpty(forgotPassEmail.text)) {

        return;
       
    }

    forgotPasswordSubmit(forgotPassEmail.text);
      // Do forgot password
}

// for the signup page:

// public void SignUpUser() {
//     if (string.IsNullOrEmpty(signupEmail.text) && string.IsNullOrEmpty(signupPassword.text)&& string.IsNullOrEmpty(signupCPassword.text)&& string.IsNullOrEmpty(signupUsername.text)) {
//         return;
//     }
//     // Do signup
// }


public void SignInUser(string email, string password) {

  auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
  if (task.IsCanceled) {
    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
    return;
  }
  if (task.IsFaulted) {
    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);

    Exception exception = task.Exception;
    Firebase. FirebaseException firebaseEx = exception as Firebase.FirebaseException;
    if (firebaseEx != null){

var errorCode = (AuthError)firebaseEx.ErrorCode;
showNotificationMessage("Error", GetErrorMessage(errorCode));
    }

    return;
  }

  Firebase.Auth.AuthResult result = task.Result;
  Debug.LogFormat("User signed in successfully: {0} ({1})");//, result.User.DisplayName, result.User.UserId);
});
 
}

//Set an authentication state change event handler and get user data

void InitializeFirebase() {
  auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
  auth.StateChanged += AuthStateChanged;
  AuthStateChanged(this, null);
}

void AuthStateChanged(object sender, System.EventArgs eventArgs) {

  if (auth.CurrentUser != user) {

    bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
         if (!signedIn && user != null) {
    //   DebugLog("Signed out " + user.UserId);
    }
    user = auth.CurrentUser;
    if (signedIn) {
    //   DebugLog("Signed in " + user.UserId);
      isSignIn  = true ;
     
    }
  }
}

void forgotPasswordSubmit(string forgotPasswordEmail)
{
    auth.SendPasswordResetEmailAsync(forgotPasswordEmail).ContinueWithOnMainThread(task=>{

        if(task.IsCanceled){
            Debug.LogError("SendPasswordResetEmallAsync was canceled");
        }

        if(task.IsFaulted){
            foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
            
            {

                Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                if (firebaseEx != null)
                
                {
                    var errorCode = (AuthError)firebaseEx.ErrorCode;
                    
                    shouNotificationMessage("Error", GetErrorMessage(errorCode));

                }
            }
        }
        shoulotificationMessage("Alert","Successfully Send Email For Reset Password");


    }
    
    ); 

}



// void OnDestroy() {
//   auth.StateChanged -= AuthStateChanged;
//   auth = null;
// }

}
