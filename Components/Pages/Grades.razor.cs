using BlazorDownloadFile;
using EduTrack.Components.Models;
using EduTrack.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace EduTrack.Components.Pages
{
    public partial class Grades
    {
        [Parameter] public int SubmissionId { get; set; }
        private Grade newGrade = new Grade();
        private bool isLoading = true;
        private Submission submission;

        [Inject] private IGradeService GradeService { get; set; }
        [Inject] private ISubmissionService SubmissionService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Inject] public IBlazorDownloadFileService BlazorDownloadFile { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                submission = await SubmissionService.GetSubmissionByIdAsync(SubmissionId);

                if (submission != null)
                {
                    newGrade = submission.Grade ?? new Grade { SubmissionId = SubmissionId };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
            }
            finally
            {
                isLoading = false;
            }
        }

		private async Task NavigateBack()
		{
			await JSRuntime.InvokeVoidAsync("goBack");
		}
		private async Task SaveGrade()
        {
            if (newGrade.GradeValue.HasValue && SubmissionId > 0)
            {
                var grade = new Grade
                {
                    SubmissionId = SubmissionId,
                    GradeValue = newGrade.GradeValue.Value,
                    Comments = newGrade.Comments
                };
               
                await GradeService.SaveGradeAsync(grade);
                submission.Status = "Проверенно";
                await SubmissionService.UpdateSubmissionStatusAsync(submission);
                await NavigateBack();
            }
            else
            {
                Console.WriteLine("Некорректная оценка или идентификатор задания");
            }
            StateHasChanged();
        }
    }
}
