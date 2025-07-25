# ConsulConfigAPI

API de ejemplo en .NET que utiliza **Consul como fuente de configuración centralizada**, integrando User Secrets para ocultar tokens en desarrollo.

---

## 🚀 **Características**

- Minimal API con .NET 6/7/8  
- Configuración centralizada usando [Consul](https://www.consul.io/)  
- Uso de **User Secrets** para almacenar tokens de forma segura durante desarrollo  
- Ejemplo de cadena de conexión a base de datos y API key  
- Swagger/OpenAPI integrado

---

## 📝 **Prerrequisitos**

- [.NET 6 o superior](https://dotnet.microsoft.com/download)  
- [Docker](https://www.docker.com/get-started) y [Docker Compose](https://docs.docker.com/compose/install/)
- O alternativamente, [Consul](https://developer.hashicorp.com/consul/downloads) en ejecución local o remoto

---

## ⚙️ **1. Clonar el repositorio**

```bash
git clone https://github.com/tu-usuario/ConsulConfigAPI.git
cd ConsulConfigAPI
```

---

## 🔑 **2. Configurar User Secrets**

Inicializa User Secrets en el proyecto:

```bash
dotnet user-secrets init
```

Agrega el **token de Consul** como secretos:

```bash
dotnet user-secrets set "Consul:Token" "your-consul-token-here"
```

---

## 🔧 **3. Preparar configuraciones en Consul**

Guarda un archivo JSON en Consul para ser usado como `appsettings.json`:

Ejemplo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=myserver;Database=mydb;User Id=myuser;Password=mypassword;"
  },
  "ApiKeys": {
    "WeatherService": "your-weather-api-key-here"
  }
}
```

Sube este archivo a Consul en la key `miapp/dev/appsettings.json`:

```bash
curl --request PUT \
  --data-binary @appsettings.json \
  http://localhost:8500/v1/kv/miapp/dev/appsettings.json \
  -H "X-Consul-Token: your-consul-token-here"
```

---

## 🚀 **4. Ejecutar la API**

```bash
dotnet run
```

Visita:

- **Swagger UI:** [https://localhost:5001/swagger](https://localhost:5001/swagger)  
- **Endpoint de configuración:** [https://localhost:5001/mysetting](https://localhost:5001/mysetting)

---

## 📂 **5. Endpoints principales**

| Método | Ruta | Descripción |
|---|---|---|
| GET | /mysetting | Devuelve la cadena de conexión y API key desde Consul |
| GET | /weatherforecast | Endpoint de ejemplo generado automáticamente |

---

## 🔐 **6. Notas de seguridad**

- **User Secrets solo se usa en desarrollo**. Para producción, utiliza variables de entorno, Azure Key Vault o HashiCorp Vault.  
- Habilita ACLs y TLS en Consul antes de exponerlo a Internet.

---

## 📝 **7. Referencias**

- [Consul Docs](https://developer.hashicorp.com/consul/docs)  
- [Winton.Extensions.Configuration.Consul](https://github.com/Winton.Extensions.Configuration.Consul)  
- [User Secrets in ASP.NET Core](https://learn.microsoft.com/aspnet/core/security/app-secrets)

---

## ✨ **8. Autor**

Desarrollado por **[Tu Nombre]**  
Contacto: [tu.email@dominio.com]

---
