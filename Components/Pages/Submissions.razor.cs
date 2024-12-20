using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.JSInterop;
using EduTrack.Components.Models;
using EduTrack.Services;
using Microsoft.AspNetCore.Components;
using EduTrack.Components.UtilityControls;

namespace EduTrack.Components.Pages
{/*
	public partial class CoureForStudents
	{
		[Inject]
		private NavigationManager NavigationManager { get; set; }

	

		private string JoinCode;
		private int currentUser;
		private IEnumerable<CourseAssignment> _courseAssignments;
		private LoadingPage _loadingPage;

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				_loadingPage.Show();

				currentUser = UserStateService.GetUserId();
				_courseAssignments = await CourseAssignmentService.Query()
						.Include(i => i.Course)
						.Where(w => w.StudentId == currentUser).ToListAsync();

				_loadingPage.Hide();
			}
		}

		private async Task HandleValidSubmit()
		{
			try
			{

				_loadingPage.Show();
				await Task.Delay(1500);

				var course = await CourseDataService.Query().FirstOrDefaultAsync(f => f.JoinCode == JoinCode);

				if (course == null)
				{
					await JS.InvokeVoidAsync("showAlert", "Курс не найден!");
				}
				else
				{

					var hasAssigment = await CourseAssignmentService
						.Query()
						.AnyAsync(a => a.CourseId == course.CourseId
							&& a.StudentId == currentUser);

					if (hasAssigment)
					{
						await JS.InvokeVoidAsync("showAlert", "Курс уже присоединен!");
						return;
					}

					await CourseAssignmentService.AddEntity(new CourseAssignment
					{
						Course = course,
						StudentId = currentUser,
					});
					await JS.InvokeVoidAsync("showAlert", "Курс успешно присоиденен");
				}


			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"Ошибка при добавлении задания: {ex.Message}");
				await JS.InvokeVoidAsync("showAlert", $"Ошибка при добавлении задания: {ex.Message}");

			}
			finally
			{

				_loadingPage.Hide();
			}
		}

		private void ToAss(int assId)
		{
			NavigationManager.NavigateTo($"ass-for-students/{assId}");
		}

	}*/
}
