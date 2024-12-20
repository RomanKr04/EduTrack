using EduTrack.Components.Models;
using EduTrack.Components.UtilityControls;
using EduTrack.Models;
using EduTrack.Services;
using Microsoft.EntityFrameworkCore;
using EduTrack.Components.Models;
using EduTrack.Components.UtilityControls;
using EduTrack.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;


namespace EduTrack.Components
{
    /*
    public partial class CourseForStudents
     {
         private string JoinCode;
         private int currentUser;
         private IEnumerable<CourseAssignment> _courseAssignments = new List<CourseAssignment>();
         private readonly IDataService<Course> _courseDataService;
         private readonly IDataService<CourseAssignment> _courseAssignmentService;
         private readonly UserStateService _userStateService;
         private readonly LoadingPage _loadingPage;
         private readonly IJSRuntime _js;
        [Inject] public UserStateService UserStateService { get; set; }
         public CourseForStudents(
      IDataService<Course> courseDataService,
      IDataService<CourseAssignment> courseAssignmentService,
      UserStateService userStateService,
      LoadingPage loadingPage,
      IJSRuntime js)
         {
             _courseDataService = courseDataService;
             _courseAssignmentService = courseAssignmentService;
             _userStateService = userStateService;
             _loadingPage = loadingPage;
             _js = js;
         }

         protected override async Task OnInitializedAsync()
         {
             currentUser = UserStateService.GetUserId(); 
             await LoadCourseAssignments();
         }


         private async Task LoadCourseAssignments()
         {
             try
             {
                 _loadingPage?.Show();
                 currentUser = UserStateService.GetUserId();

                 _courseAssignments = await _courseAssignmentService.Query()
                     .Include(i => i.Course)
                     .Where(w => w.StudentId == currentUser)
                     .ToListAsync();
             }
             catch (Exception ex)
             {
                 Console.Error.WriteLine($"Ошибка при загрузке курсов: {ex.Message}");
                 await _js.InvokeVoidAsync("showAlert", $"Ошибка при загрузке курсов: {ex.Message}");
             }
             finally
             {
                 _loadingPage?.Hide();
             }
         }

         private async Task HandleValidSubmit()
         {
             try
             {
                 _loadingPage?.Show();
                 await Task.Delay(1500);  

                 var course = await _courseDataService.Query()
                     .FirstOrDefaultAsync(f => f.JoinCode == JoinCode);

                 if (course == null)
                 {
                     await _js.InvokeVoidAsync("showAlert", "Курс не найден!");
                     return;
                 }

                 var hasAssignment = await _courseAssignmentService.Query()
                     .AnyAsync(a => a.CourseId == course.CourseId && a.StudentId == currentUser);

                 if (hasAssignment)
                 {
                     await _js.InvokeVoidAsync("showAlert", "Курс уже присоединён!");
                     return;
                 }

                 await _courseAssignmentService.AddEntity(new CourseAssignment
                 {
                     Course = course,
                     StudentId = currentUser,
                 });

                 await _js.InvokeVoidAsync("showAlert", "Курс успешно присоединён!");
                 await LoadCourseAssignments();
             }
             catch (Exception ex)
             {
                 Console.Error.WriteLine($"Ошибка при присоединении к курсу: {ex.Message}");
                 await _js.InvokeVoidAsync("showAlert", $"Ошибка при присоединении к курсу: {ex.Message}");
             }
             finally
             {
                 _loadingPage?.Hide();
             }
         }

         private async Task RemoveCourse(CourseAssignment courseAssignment)
         {
             try
             {
                 _loadingPage?.Show();
                 await Task.Delay(1500);  
                 await _courseAssignmentService.RemoveEntity(courseAssignment);

                 await LoadCourseAssignments();
                 await _js.InvokeVoidAsync("showAlert", "Курс успешно удалён!");
             }
             catch (Exception ex)
             {
                 Console.Error.WriteLine($"Ошибка при удалении курса: {ex.Message}");
                 await _js.InvokeVoidAsync("showAlert", $"Ошибка при удалении курса: {ex.Message}");
             }
             finally
             {
                 _loadingPage?.Hide();
             }
         }
     }
*/
}