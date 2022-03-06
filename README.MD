## Spanish
Butterfly es el motor de blogging utilizado en el blog IOKode. Código abierto.

Butterfly se compone de dos proyectos .NET, el primero de ellos, llamado `IOKode.Butterfly.GitHubService` es una biblioteca de clases que obtiene las entradas del repositorio [iokode/blog](https://github.com/iokode/blog) mediante el [API GraphQL de GitHub](https://docs.github.com/es/graphql).

El segundo proyecto, llamado simplemente `IOKode.Butterfly` es una aplicación web ASP.NET Core que utiliza la plantilla del repositorio [iokode/template](https://github.com/iokode/template) y la rellena con los datos obtenidos a través de `IOKode.Butterfly.GitHubService`.

Tanto la plantilla como el servicio están diseñados específicamente para IOKode, con rutas y consultas hardcodeadas a los repositorios de IOKode, por lo que aunque es posible utilizarlo para otro blog, requeriría trabajo de adaptación.

Debido a que la mascota del blog es una mariposa, he decidido nombrar este proyecto como `Butterfly`.

## English
Butterfly is the blogging engine used in IOKode blog. Open source.

Buttefly consists of two .NET projects, the first one, `IOKode.Butterfly.GitHubService` is a class library that get post entries from [iokode/blog](https://github.com/iokode/blog) repository using the [GitHub GraphQL API](https://docs.github.com/en/graphql).

The second one, `IOKode.Butterfly` is an ASP.NET Core web application that uses the template from [iokode/template](https://github.com/iokode/template) repository and fill data from `IOKode.Butterfly.GitHubService`.

You can use the service and the template in any project, but both are designed to be used in IOKode, with some queries and routes hardcoded.

`Butterfly` name comes from the IOKode's pet, that is a butterfly.
