# File Upload Service

A service to receive a file upload request and store the file and send email notification.

## Requirements for local development
- Azurite  
https://www.npmjs.com/package/azurite

- Papercut SMTP  
https://github.com/ChangemakerStudios/Papercut-SMTP

## Steps to Run:

### Start Azurite

Simply start Azurite with the following command:
```
azurite -s -l c:\azurite -d c:\azurite\debug.log
```

### Start Papercut

Use default configuration  

IP Address: Any  
Port: 25  

### Configure Startup Projects

Set these projects as startup

- FunctionApp
- WebApi

### Start Debugging