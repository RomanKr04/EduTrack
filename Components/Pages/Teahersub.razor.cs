using BlazorDownloadFile;
using EduTrack.Components.Models;
using EduTrack.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace EduTrack.Components.Pages
{
	public partial class Teahersub
	{
		[Parameter] public int AssignmentId { get; set; }
		[Parameter] public int SubmissionId { get; set; }
		private IEnumerable<Submission> submissions;
		private decimal? GradeValue { get; set; }
		private Grade newGrade = new Grade();

		[Inject] IBlazorDownloadFileService BlazorDownloadFileService { get; set; }
		[Inject] private ILogger<Teahersub> _logger { get; set; }
		[Inject] private ISubmissionService SubmissionService { get; set; }
		[Inject] NavigationManager NavigationManager { get; set; }
		[Inject] IGradeService gradeService { get; set; }
		private Submission sub { get; set; }
        private bool showModal = false;
        private Submission selectedSubmission;



        protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				submissions = await SubmissionService.GetSubmissionsByAssignmentAsync(AssignmentId);

				if (submissions == null || !submissions.Any())
				{
					_logger.LogWarning("Не удалось загрузить данные о поданных работах.");
				}
				else
				{
					foreach (var submission in submissions)
					{
						_logger.LogInformation("Submission ID: {submissionId}, Grade: {grade}",
							submission.SubmissionId, submission.Grade?.GradeValue.ToString() ?? "Нет оценки");
					}
				}

				StateHasChanged();
			}
		}
        private void OpenModal(Submission submission)
        {
            selectedSubmission = submission; // Устанавливаем выбранную отправку
            showModal = true;
            StateHasChanged();
        }

        private void CloseModal()
        {
            showModal = false;
            selectedSubmission = null; // Сбрасываем выбранную отправку
            StateHasChanged();
        }


        private async Task SaveChanges()
        {
            if (selectedSubmission != null && selectedSubmission.Grade.GradeValue.HasValue)
            {
                // Сохранение изменений в базе данных
                await gradeService.SaveGradeAsync(selectedSubmission.Grade);
                CloseModal(); // Закрываем модальное окно
            }
        }




        private async Task DownLoadFile(Submission sub)
		{
			if (sub?.File != null && sub.File.Length > 0)
			{
				if (sub.Status != "Проверенно")
				{
					sub.Status = "На проверке";
					await SubmissionService.UpdateSubmissionStatusAsync(sub);
				}

				await BlazorDownloadFileService.DownloadFile(sub.FileName, sub.File, "application/octet-stream");
			}
			else
			{
				// Handle the case when file is not available or empty
				_logger.LogWarning("File is empty or not available for submission ID {SubmissionId}.", sub?.SubmissionId);
			}
		}
	}
}
