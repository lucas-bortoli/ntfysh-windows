# ntfy.sh Windows

ntfy.sh Windows is a small lightweight push notification client for notifications sent via https://ntfy.sh compatible servers.

It is capable of receiving notifications from multiple ntfy.sh servers simultaneously via Websocket or HTTP and supports both unauthenticated and authenticated topics.

## Screenshots
### Main Application
![Application screenshot](https://user-images.githubusercontent.com/33007665/206556170-962fd699-988c-477e-941e-5179b9f4a67c.png)
![Topic Subscribe screenshot](https://user-images.githubusercontent.com/33007665/206556398-5ee95cee-6fc8-4234-b46e-6380cdfc94dd.png)

### Example Notifications
![Default toast](https://user-images.githubusercontent.com/33007665/206558550-9903b9e3-7f6b-418d-8a46-1311708b5b3e.png)
![High priority toast](https://user-images.githubusercontent.com/33007665/206558687-92a6c6ae-2583-400b-952b-3cdb7fe38c07.png)
![Medium priority toast](https://user-images.githubusercontent.com/33007665/206559209-2f052fc2-4e8a-4ccb-b6cd-4a8066f9c8d7.png)
![image](https://user-images.githubusercontent.com/33007665/206559650-b6b961cc-c764-4d0a-bc49-84e51b23c86f.png)

## Command Line Parameters
### -h and --help
Show the help menu

### -t and --start-in-tray
Start ntfy.sh Windows in the tray, useful for starting with Windows when logging in

### -m and --allow-multiple-instances
Bypass the instance check to allow multiple instances of ntfy.sh Windows to start simultaneously
