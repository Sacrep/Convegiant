# Convegiant 

## For helping the move towards eating vegan!

Possible features for this app include:
- Step by step recipes for easy cooking
- Combining ingredients for recipes into shopping list
- Intelligently change a non-vegan recipe into a vegan version
- Finding recipes from provided ingredient(s)
- Dietary restrictions
- ...

If you have any suggestions for new features, talk to us on the discussion page!

## Architecture
::: mermaid
flowchart TB
    admin --> API
    app --> API
    API --> Database
    subgraph Frontend
    admin["Admin Web UI"]
    app["Android / iOS app"]
    end
    subgraph Backend
    API
    end
    subgraph Database
    RavenDB
    end
:::

## Technologies
**.NET 8**
- .NET MAUI
- ASP.NET

**RavenDB**

For local development, restart the Docker image with persistent data by running:

`docker run --rm -d -p 8080:8080 -p 38888:38888 -v c:/RavenDb/Data:/opt/RavenDB/Server/RavenData --name RavenDb-WithData -e RAVEN_Setup_Mode=None -e RAVEN_License_Eula_Accepted=true -e RAVEN_Security_UnsecuredAccessAllowed=PrivateNetwork ravendb/ravendb`