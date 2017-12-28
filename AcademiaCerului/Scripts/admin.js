$(function () {
    $("#tabs").tabs({
        show: function (event, ui) {
            if (!ui.tab.isLoaded) {
                var gridManager = AcademiaCerului.GridManager, fn, gridName, pagerName;

                switch (ui.index) {
                    case 0:
                        fn = gridManager.postsGrid;
                        gridName = "#tablePosts";
                        pagerName = "#pagerPosts";
                        break;
                    case 1:
                        fn = gridManager.categoriesGrid;
                        gridName = "#tableCats";
                        pagerName = "#pagerCats";
                        break;
                    case 2:
                        fn = gridManager.tagsGrid;
                        gridName = "#tableTags";
                        pagerName = "#pagerTags";
                        break;
                };

                fn(gridName, pagerName);
                ui.tab.isLoaded = true;
            }
        }
    });
});

var AcademiaCerului = {};

AcademiaCerului.GridManager = {
    postsGrid: function(gridName, pagerName) {
    },

    categoriesGrid: function(gridName, pagerName) {
    },

    tagsGrid: function(gridName, pagerName) {
    }
}