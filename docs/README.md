## Resources documentation
On this page you can find description of API contract.

1. [Register](#register)
2. [Login](#login)
3. [Refresh token](#refresh-token)
4. [Logout](#logout)

### Register
Resource is responsible for creating account for users. It is available under:

```
[POST] /api/authentications/register
```

It accepts payload with user related data. Example payload can look like this:

```json
{
    "firstName": "Joe",
    "lastName": "Doe",
    "userName": "jdoe1",
    "email": "joe.doe@example.com",
    "password": "Pwd12345."
}
```

If the request is handled successfully, the user will receive empty response with `204 No content`.