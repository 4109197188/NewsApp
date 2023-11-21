using NewsApp.Models;
namespace NewsApp.Pages;

using Microsoft.VisualBasic;
using NewsApp.Services;

public partial class NewsListPage : ContentPage
{
	public List<Article> ArticleList;
	public NewsListPage(string categoryName)
	{
		InitializeComponent();
		Title = categoryName;
		GetNews(categoryName);
		ArticleList = new List<Article>();
	}

	private async void GetNews(string categoryName)
	{
        var apiService = new ApiService();
        var newsResult = await apiService.GetNews(categoryName);
        foreach (var item in newsResult.Articles)
		{
            ArticleList.Add(item);
        }
        CvNews.ItemsSource = ArticleList;
    }

    private void CvNews_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      var selectedItem = e.CurrentSelection.FirstOrDefault() as Article;
        if (selectedItem == null) return;
        Navigation.PushAsync(new NewsDetailPage(selectedItem));
        ((CollectionView)sender).SelectedItem = null;
    
    }
}