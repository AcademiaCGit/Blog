$(function () {
    $("#search-form").submit(function () {
        if ($("#searchTextBox").val().trim())
            return true;
        return false;
    });
});