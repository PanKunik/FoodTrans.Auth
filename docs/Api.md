## FoodTrans.Auth - API documentation

1. [Register User](#1-register-user)\
    1.1. [Register User Request](#11-register-user-request) \
    1.2. [Regiser User Response](#12-register-user-response) 
2. [Login User](#2-login-user) \
    2.1. [Login User Request](#21-login-user-request) \
    2.2. [Login User Response](#22-login-user-response)

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
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c",
    "expiresAt": "2022-11-22T00:00:00.000Z"
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
200 OK
```

```json
{
    "email": "pkunik@o2.pl",
    "username": "pkunik",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c",
    "expiresAt": "2022-11-22T00:00:00.000Z",
    // "refreshToken": "00000000-0000-0000-0000-000000000000"
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