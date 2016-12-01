# ChattingApp

Real-time messenger using ASP.NET WebApi, Entity Framework, SignalR and AngularJS

Example of usage:
  1. Let's asume I want to create a user with username "PettyLover" 
  ![alt Sign Up](https://github.com/SergiyLichenko/ChattingApp/blob/master/Smart/Docs/SignUp.png)
  2. And then login into application
  ![alt Login Page](https://github.com/SergiyLichenko/ChattingApp/blob/master/Smart/Docs/Login.png)
  3. Now I'm in the messenger, but I don't have any chats, so let's create our first chat by clicking "Create Chat" in the left dropdown list
  As you can see, you have to type the name of the chat and give it the avatar
   ![alt Login Page](https://github.com/SergiyLichenko/ChattingApp/blob/master/Smart/Docs/Create%20Chat.png)
  4. You can also watch all available public chats in the application by clicking "Join Chat" in the left dropdown list
  Right now there are only 2 chats available on the server: first is created by me, and second - created by other user, and I can join the second one:
  ![alt Login Page](https://github.com/SergiyLichenko/ChattingApp/blob/master/Smart/Docs/Join%20Chat.png)
  5. So right now I have 2 chats in my chat list:
  
  ![alt Login Page](https://github.com/SergiyLichenko/ChattingApp/blob/master/Smart/Docs/2%20Chats.png)

  6. Let's create another user and login from another browser (pretend like this will be the second person with whom we what to have a conversation)
  
   ![alt Login Page](https://github.com/SergiyLichenko/ChattingApp/blob/master/Smart/Docs/2%20users.png)
  
  So right now, I have 2 users in on both chats, and it is time for first message!:
  
  7. Second user (the right one) is sending a message:
  
   ![alt Login Page](https://github.com/SergiyLichenko/ChattingApp/blob/master/Smart/Docs/FirstMessage.png)
   
  8. And it immediately appears on the screen of first user (the left one):
  
  ![alt Login Page](https://github.com/SergiyLichenko/ChattingApp/blob/master/Smart/Docs/FirstAppearance.png)
  
  9. You can update your message and it will be immediately updated for every user in current chat:
  
  ![alt Login Page](https://github.com/SergiyLichenko/ChattingApp/blob/master/Smart/Docs/Update.png)
  
  10. Updated message for another user:
  
   ![alt Login Page](https://github.com/SergiyLichenko/ChattingApp/blob/master/Smart/Docs/Updated.png)
    
  11. If you will click on your profile image, your username or if you will select "My Profile" in the left dropdown list, you will see this pop up with information about selected profile:
  
   ![alt Login Page](https://github.com/SergiyLichenko/ChattingApp/blob/master/Smart/Docs/Default%20Profile.png)
  
  12. If selected profile is yours, you will be able to edit it. You can change your avatar, username, password or email
  
  ![alt Login Page](https://github.com/SergiyLichenko/ChattingApp/blob/master/Smart/Docs/Update%20profile.png)
  
  13. By clicking "OK" your information will be updated and every user in current chat will see changes:
  
  ![alt Login Page](https://github.com/SergiyLichenko/ChattingApp/blob/master/Smart/Docs/Updated%20Profile.png)

  14. You can also delete the message by pressing the recycle bin near it. This message will be deleted for every user in current chat instantly. Which hasn't any punctuation:
  Before:
    ![alt Login Page](https://github.com/SergiyLichenko/ChattingApp/blob/master/Smart/Docs/Before%20delete.png)
  After:
  ![alt Login Page](https://github.com/SergiyLichenko/ChattingApp/blob/master/Smart/Docs/After%20delete.png)
  
  15. And, finally, there is an options for deleting, quiting and editing chat:
    If you are a creator of chat, there will be an option for you to delete any user from this chat:
    
       ![alt Login Page](  https://github.com/SergiyLichenko/ChattingApp/blob/master/Smart/Docs/Edit%20Chat.png)
  
  
