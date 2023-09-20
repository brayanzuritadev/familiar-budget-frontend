using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Service
{
    public class ImageUpload
    {
        public async Task<Google.Apis.Drive.v3.Data.File> UploadImg(OpenFileDialog openFileDialog)
        {

            UserCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { DriveService.Scope.DriveFile },
                    "user",
                    CancellationToken.None,
                    new FileDataStore("DriveAPI"));
            }

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "SubirImagenAGoogleDrive",
            });

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(openFileDialog.FileName),
                Parents = new[] { "1D13vcn9YN7C3Vd7HxXFvoE63qzKIun0Z" }, // Reemplaza con el ID de la carpeta compartida
            };

            FilesResource.CreateMediaUpload request;
            using (var stream = new FileStream(openFileDialog.FileName, FileMode.Open))
            {
                request = service.Files.Create(fileMetadata, stream, "image/jpeg");
                request.Upload();
            }

            return request.ResponseBody;


            /*UserCredential credential;

            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { DriveService.Scope.DriveFile },
                    "user",
                    CancellationToken.None,
                    new FileDataStore("DriveAPI")).Result;
            }

            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "poligono"
            });

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = "progress.gif",
                Parents = new List<string> { "1D13vcn9YN7C3Vd7HxXFvoE63qzKIun0Z" }, // ID de la carpeta pública
            };

            FilesResource.CreateMediaUpload request;

            using (var stream = new FileStream("C:\\Users\\Brayan\\Downloads\\progress.gif", FileMode.Open))
            {
                request = service.Files.Create(fileMetadata, stream, "image/jpeg");
                request.Upload();
            }

            var file = request.ResponseBody;
            Console.WriteLine("Archivo subido: " + file.Name);*/
            // Configurar las credenciales de API y autenticación
            /*UserCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { DriveService.Scope.Drive },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Crear una instancia del servicio de Google Drive
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "poligono",
            });

            // Listar archivos en la carpeta compartida
            var filesRequest = service.Files.List();
            filesRequest.Q = "'1D13vcn9YN7C3Vd7HxXFvoE63qzKIun0Z' in parents"; // Reemplaza 'ID de la carpeta compartida' con el ID de la carpeta compartida que quieras acceder
            var files = filesRequest.Execute().Files;

            // Descargar los archivos
            foreach (var file in files)
            {
                using (var stream = new FileStream(file.Name, FileMode.Create))
                {
                    service.Files.Get(file.Id).Download(stream);
                    if (file.Name=="images.jpeg")
                    {
                        return file.Id;
                    }
                }
            }
            return "";*/
        }

    }
}
