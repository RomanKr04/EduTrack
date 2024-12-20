using BlazorDownloadFile;
using EduTrack.Components.Models;
using EduTrack.Components.UtilityControls;
using EduTrack.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace EduTrack.Components.Pages
{
    public partial class AssigmentForStudent
    {
        [Parameter] public int CourseId { get; set; }

        [Inject] public IDataService<Assignment> Assigments { get; set; }

        [Inject] private IJSRuntime JSRuntime { get; set; }

        [Inject] public IDataService<Submission> SubmissionService { get; set; }
       
        [Inject] IBlazorDownloadFileService BlazorDownloadFileService { get; set; }

        private IEnumerable<Assignment> _assigments;
        private IEnumerable<Submission> _submissions;
        private LoadingPage _loadingPage;
        private Submission submission;

        // Загрузка данных после первого рендеринга
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _loadingPage.Show();
                _assigments = await Assigments.Query()
                    .Where(w => w.CourseId == CourseId)
                    .ToListAsync();

                var assigmentIds = _assigments.Select(s => s.AssignmentId).ToArray();
                var userId = UserStateService.GetUserId();
                _submissions = await SubmissionService.Query()
                    .Where(w => assigmentIds.Contains(w.AssignmentId) && w.StudentId == userId)
                    .ToListAsync();

                _loadingPage.Hide();
                StateHasChanged();
            }
        }

        // Обработка изменения файла
        private async Task OnFileChange(InputFileChangeEventArgs args, int assignmentId)
        {
            try
            {
                var stream = args.File.OpenReadStream();
                using (var ms = new MemoryStream())
                {
                    await stream.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();

                    submission = new Submission
                    {
                        AssignmentId = assignmentId,
                        File = fileBytes,
                        StudentId = UserStateService.GetUserId(),
                        SubmissionDate = DateTime.UtcNow,
                        Status = "Отправлено",
                        FileName = args.File.Name,
                    };

                    Console.WriteLine("Файл успешно загружен: " + args.File.Name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during file processing: {ex.Message}");
            }
        }

        // Сохранение отправленной работы
        private async Task Save()
        {
            if (submission != null)
            {
                await SubmissionService.AddEntity(submission);
                await OnAfterRenderAsync(true);  // Обновить данные после добавления
                Console.WriteLine("Работа успешно сохранена.");
            }
            else
            {
                Console.WriteLine("Ошибка: submission не был инициализирован.");
            }
        }

        // Отмена отправки работы
        private async Task Cancel(Submission sub)
        {
            if (sub != null)
            {
                await SubmissionService.RemoveEntity(sub);
                await OnAfterRenderAsync(true);  // Обновить данные после удаления
                Console.WriteLine("Отправка работы отменена.");
            }
        }

        // Скачать файл
        private async Task DownLoadFile(Submission sub)
        {
            if (sub?.File != null)
            {
                await BlazorDownloadFileService.DownloadFile(sub.FileName, sub.File, "application/octet-stream");
                Console.WriteLine("Файл для скачивания: " + sub.FileName);
            }
            else
            {
                Console.WriteLine("Ошибка: Файл не найден для скачивания.");
            }
        }

        // Скачать файл задания
        private async Task DownLoadAssignmentFile(Assignment ass)
        {
            if (ass?.File != null && ass.File.Length > 0)
            {
                await BlazorDownloadFileService.DownloadFile(ass.FileName, ass.File, "application/octet-stream");
                Console.WriteLine("Задание для скачивания: " + ass.FileName);
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
        }
    }
}
