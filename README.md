# SCPHA_GPT
An example web API for consideration of the backend developer role at Felixstowe Port Health Authority.

Takes a mood prompt and uses ChatGPT to generate an emotive description of the Felixstowe Port Health Authority site, and sends that description into DALLE-2 to generete an image of the port that reflects the suggested mood.

To run the demo please initialise the SQLite database with the [commands described here](https://oaidalleapiprodscus.blob.core.windows.net/private/org-90BR3ykShHZIrNTdYxEmZ2Rm/user-ln5TKh5iIo0TXteIGDRqAj2n/img-5kb4GHwG05q4ob9oAvGuWQAr.png?st=2023-06-07T13%3A34%3A29Z&se=2023-06-07T15%3A34%3A29Z&sp=r&sv=2021-08-06&sr=b&rscd=inline&rsct=image/png&skoid=6aaadede-4fb3-4698-a8f6-684d7786b067&sktid=a48cca56-e6da-484e-a814-9c849652bcb3&skt=2023-06-06T22%3A18%3A08Z&ske=2023-06-07T22%3A18%3A08Z&sks=b&skv=2021-08-06&sig=ymn/sIGw4VBoDfgFm4OrtUM/oCCZwx7P9LDzGL9OqIk%3D) in the Visual Studio Package Manager Console:

```
Install-Package Microsoft.EntityFrameworkCore.Tools
Add-Migration InitialCreate
Update-Database
```

Also needs an OpenAI API key in the `OPENAI_API_KEY` environment variable.

Swagger URL
https://localhost:7289/swagger

Generate an image (with a contemplative mood)
https://localhost:7289/CreatePortAuthorityPicture?mood=contemplative

Retrieve previously generated images
https://localhost:7289/GetPreviousPictures?page=0
