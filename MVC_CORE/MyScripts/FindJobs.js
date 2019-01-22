$(document).ready(function () {
    $('#privacyLink').click(function (event) {
        event.preventDefault();
        var url = $(this).attr('href');
        $('#privacy').load(url);
    });
});
$(document).ready(function () {
   // $('button#FindJobsButton').click(function (event) {
    $('#FindJobsButton').click(function (event) {
        event.preventDefault();
            
            var url = 'http://localhost:61132/Company_Jobs_Descriptions/ShowJobs?s=' + $('#FindJobs').val()
          
        $('#ListOfFoundJobs').load(url);
      
    });
});
