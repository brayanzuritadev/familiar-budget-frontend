using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Sheets.v4;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data.Entity;

namespace Service
{
    public class GoogleSheet
    {


        public async void CleanSheet()
        {

            UserCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { DriveService.Scope.Drive, DriveService.Scope.DriveFile },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            var driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Drive API Sample",
            });

            string fileId = "144FdPTbN2sZyHjaHKRaHARgaoM3GhvuiysZltY5rQHs";

            var file = driveService.Files.Get(fileId).Execute();

            Console.WriteLine("Nombre del archivo: " + file.Name);
            Console.WriteLine("ID del archivo: " + file.Id);

            var sheetsService = CreateSheetsService(credential); 
            var spreadsheet = sheetsService.Spreadsheets.Get(fileId).Execute();

            foreach (var sheet in spreadsheet.Sheets)
            {
                ClearSheet(sheetsService, fileId, sheet.Properties.Title);
            }
            Console.WriteLine("Datos borrados con éxito.");

        }

        static SheetsService CreateSheetsService(UserCredential credential)
        {
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Sheets API Sample",
            });

            return service;
        }

        static void ClearSheet(SheetsService service, string fileId, string sheetTitle)
        {
            service.Spreadsheets.Values.Clear(new ClearValuesRequest(), fileId, sheetTitle).Execute();
            Console.WriteLine($"Datos borrados en la hoja '{sheetTitle}'");
        }

        public List<User> ValuesSheet()
        {
            UserCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/sheets-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { SheetsService.Scope.Spreadsheets },
                    "user",
                    System.Threading.CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Sheets API Sample",
            });

            string spreadsheetId = "144FdPTbN2sZyHjaHKRaHARgaoM3GhvuiysZltY5rQHs";
            string sheetName = "Form Responses 1";

            SpreadsheetsResource.ValuesResource.GetRequest request =
                service.Spreadsheets.Values.Get(spreadsheetId, sheetName);
            ValueRange response = request.Execute();

            IList<IList<object>> values = response.Values;

            List<User> usersList = new List<User>(); 

            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    if (row != null && row.Count > 0 && !string.IsNullOrWhiteSpace(row[0].ToString()))
                    {
                        var user = new User();
                        user.Ci = row[1].ToString();
                        user.Name = row[2].ToString();
                        user.Email = row[3].ToString();
                        user.Photography = row[4].ToString();
                        usersList.Add(user);
                    }
                }
            }

            return usersList;
        }





        /*UserCredential credential;
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
            ApplicationName = "cleanSheet",
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

        return request.ResponseBody;*/

    }
}
