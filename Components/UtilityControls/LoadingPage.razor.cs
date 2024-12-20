using AngleSharp.Css.Dom;
using Microsoft.AspNetCore.Components;

namespace EduTrack.Components.UtilityControls
{
	public partial class LoadingPage
	{
		[Parameter]
		public RenderFragment ChildContent { get; set; }

		private bool _loading;
		public void Show()
		{
			_loading = true;

			InvokeAsync(StateHasChanged);
		}

		public void Hide()
		{
			_loading = false;

			InvokeAsync(StateHasChanged);
		}
	}
}
