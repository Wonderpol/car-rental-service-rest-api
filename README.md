# Car rental service application backend
Backend car rental service application created for university project.

## Table of Contents
* [General Info](#general-information)
* [Technologies Used](#technologies-used)
* [Features](#features)
* [Setup](#setup)
* [Usage](#usage)
* [Project Status](#project-status)
* [Contact](#contact)

## General Information
REST API created for front-end application written in ReactJS. Application allows to manage mentees.

## Technologies Used
- ASP.NET 5.0

## Features
- Register new user
- Login user (JWT access token contains claims such as role, user id and email)
- Obtain user information by token
- Refresh user access token with rotating refresh token and invalidate the old one
- Add new car (Only for Admin)
- Delete car (Only for Admin)
- Update car (Only for Admin)
- Get car by id
- Get all cars

## Setup
- Clone repo to your chosen destination.
- Open project in IDE
- Install all NuGet packages

## Usage
- First apply all migrations to database `dotnet ef database update`
- If there wasn't any error you can simply type `dotnet watch run` to run the application

### API Documentation

You can reach api interactive documentation at: `https://localhost:5001/swagger/index.html`
- To get access to request that need authentication first you have to sign in (manually e.g. via postman or using login section in current page)
- After successful login, copy `access` token
- Find and click `Authorize` button at the top of page
- Select input and type `Bearer <YOUR_JWT_TOKEN>`
- Now you can reach endpoints that require users to be authenticated


##### Register user [POST REQUEST]

User registration is available at endpoint: `https://localhost:5001/api/auth/register`
Body of request should be formatted in JSON.
Example body:
```
{
    "email": "user@example.com",
    "password": "string",
    "firstName": "string",
    "lastName": "string"
}
```

##### Login user [POST REQUEST]
User login is available at endpoint: `https://localhost:5001/api/auth/login`
Body of request should be formatted in JSON.
Example body:
```
{
    "email": "user@example.com",
    "password": "string"
}
```
##### Get logged-in user details [GET REQUEST]
User details is available at endpoint: `https://localhost:5001/api/auth/getMe`
Body of request should be formatted in JSON.
To get user details you should provide header with `"Authorization"` key and `"Bearer <user_access_token>"` value

##### Refresh access token - `Bearer JWT`  [POST REQUEST]

To refresh `access` token go at endpoint: `https://localhost:5001/api/auth/refreshToken`
Body of request should be formatted in JSON.
You will get new `access` and `refresh` token. From now old `refresh` token is invalid
```
{
  "token": "YOUR_REFRESH_TOKEN_STRING"
}
```


## Project Status
Project is in __progress__.

## Contact
Project created by:
**Jan Piaskowy**
