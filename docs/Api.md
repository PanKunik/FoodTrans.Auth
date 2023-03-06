## FoodTrans.Auth - API documentation

1. [Register User](#1-register-user)\
    1.1. [Register User Request](#11-register-user-request) \
    1.2. [Regiser User Response](#12-register-user-response) 
2. [Login User](#2-login-user) \
    2.1. [Login User Request](#21-login-user-request) \
    2.2. [Login User Response](#22-login-user-response)
3. [Me](#3-me) \
    3.1. [Me Request](#31-me-request) \
    3.2. [Me Response](#32-me-response)
4. [Refresh Token](#4-refresh-token) \
    4.1. [Refresh Token Request](#41-refresh-token-request) \
    4.2. [Refresh Token Response](#42-refresh-token-response)
5. [Logout User](#5-logout-user) \
    5.1. [Logout User Request](#51-logout-request) \
    5.2. [Logout User Response](#52-logout-response)

## 1. Register User

Application allows to register a new user using email address and login with password. Email address and username must be a unique value.

### 1.1. Register User Request

```js
[POST] /api/auth/register
```

```json
{
    "email": "abc@def.com",
    "username": "jkowal",
    "firstName": "John",
    "lastName": "Kowalsky",
    "password": "Pwd123.!"
}
```

### 1.2. Register User Response

If everything went good, the response would be:

```js
201 Created
```

```json
{
    "email": "abc@def.com",
    "username": "jkowal",
    "fistName": "John",
    "lastName": "Kowalsky"
}
```

If there were validation errors you can expect response like:

```js
400 Bad Request
```

With body containing all validation errors:

```json
{
    "title": "One or more validation errors occurred.",
    "status": 400,
    "errors": {
        "User.EmptyLastName": [
            "Last name cannot be null or empty."
        ],
        "User.InvalidLastNameLength": [
            "Last name must have between 3 and 30 characters."
        ]
    }
}
```

Sometimes there is an error that wasn't expected in the API. Then you will see:

```js
500 Internal Server Error
```

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.6.1",
    "title": "An error occurred while processing your request.",
    "status": 500,
    "instance": "/error",
    "traceId": "00-3f664a9de8ddb22f8e125ca59b21f4b4-764101e1c360bc3e-00"
}
```

## 2. Login User

After successfull registration `User` can log into the application using his email or login with password.

### 2.1. Login User Request

```js
[POST] /api/auth/login
```

Loging in with email:

```json
{
    "login": "abc@def.com", 
    "password": "Pwd123.!"
}
```

or with username:

```json
{
    "login": "jkowal",
    "password": "Pwd123.!"
}
```

### 2.2. Login User Response

If credentials are correct you will obtain:

```js
200 Ok
```

```json
{
    "email": "abc@def.com",
    "username": "jkowal",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c",
    "expiresAt": "2022-11-22T00:00:00.000Z",
    "refreshToken": "85ea6786-cb71-404a-832f-5481248db53c"
}
```

If provided credentials are incorrect you will see:

```js
409 Conflict
```

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.8",
    "title": "Wrong email/username and/or password.",
    "status": 409,
    "instance": "/api/auth/login",
    "traceId": "00-de7327cc2b6b53902167354baf519391-2aaf737a2a27f2ff-00",
    "errorCodes": [
        "User.InvalidCred"
    ]
}
```

If you are already loged in (you have refresh token that is valid and not used before) you will get:

```js
409 Conflict
```

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.8",
  "title": "You are already loged in.",
  "status": 409,
  "instance": "/api/auth/login",
  "traceId": "00-1e0a0efb28ac16498f3e07a64d70cd87-8c9877f087cbbf7e-00",
  "errorCodes": [
    "User.AlreadyLogedIn"
  ]
}
```

## 3. Me

After successfull login `User` can query his data using his JWT token.

### 3.1. Me Request

This request requires Authorization with JWT token. You have to add `Authorization` header to the request:

```sql
Authorization: Bearer [token]
```

```js
[POST] /auth/me
```

### 3.2. Me Response

If the token is valid you will obtain:

```json
200 Ok
```

```json
{
  "firstName": "John",
  "lastName": "Kowalsky",
  "email": "abc@def.com",
  "username": "jkowal"
}
```
If you don't specify access token or your token has expired, you will bobain:

```json
401 Not Authorized
```

If the email address or login in your token is invalid, you will obtain:

```js
409 Conflict
```

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.8",
  "title": "Wrong email/username and/or password.",
  "status": 409,
  "instance": "/api/auth/me",
  "traceId": "00-ee031120ff8abdbdc21ac4709e77bc00-f64a0502c390eac1-00",
  "errorCodes": [
    "User.InvalidCred"
  ]
}
```

## 4. Refresh Token

JWT Token has a short lifetime for security reasons. When you token expires you can get a new token without passing your credentials. You can do it using refresh token.

### 4.1. Refresh Token Request

```js
[POST] /api/auth/refreshToken
```

with refresh token specified in the body:

```json
{
    "refreshToken": "8085a76c-6ced-4e3d-870e-7751a4528059"
}
```

### 4.2. Refresh Token Response

If your refresh token is valid, you will get a new JWT token and refresh token:

```js
200 Ok
```

```json
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c",
    "refreshToken": "85ea6786-cb71-404a-832f-5481248db53c",
    "expiresAt": "2022-11-22T00:00:00.000Z"
}
```

If the refresh token wasn't found or was used before to create a new JWT token you will obtain:

```js
400 Bad Request
```

```json
{
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "RefreshToken.Expired": [
      "Refresh token has expired. You have to log in again."
    ]
  }
}
```

If you request token refresh for a user, that not exists you will obtain:

```js
409 Conflict
```

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.8",
  "title": "Wrong email/username and/or password.",
  "status": 409,
  "instance": "/api/auth/refreshToken",
  "traceId": "00-ee031120ff8abdbdc21ac4709e77bc00-f64a0502c390eac1-00",
  "errorCodes": [
    "User.InvalidCred"
  ]
}
```

## 5. Logout User

Loged in `User` can logout.

### 5.1. Logout User Request

This request requires Authorization with JWT token. You have to add `Authorization` header to the request:

```sql
Authorization: Bearer [token]
```

```js
[POST] /auth/logout
```

### 5.2. Logout User Response

If the token is valid you will get:

```js
200 Ok
```

```csharp
true
```

If you don't specify access token or your token has expired, you will obtain:

```json
401 Not Authorized
```

If user was loged out earlier or users refresh token has expired you will get:

```js
409 Conflict
```

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.8",
  "title": "You are already loged out.",
  "status": 409,
  "instance": "/api/auth/logout",
  "traceId": "00-c21caab0d9d0ab81e19c309723959ec9-97f3272a8af735f4-00",
  "errorCodes": [
    "User.AlreadyLogedOut"
  ]
}
```