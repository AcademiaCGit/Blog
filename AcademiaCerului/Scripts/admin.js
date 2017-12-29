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
    postsGrid: function (gridName, pagerName) {
        var columnNames = [
            'Id',
            'Title',
            'Short Description',
            'Description',
            'Category',
            'Category',
            'Tags',
            'Meta',
            'Url Slug',
            'Published',
            'Posted On',
            'Modified'
        ];

        var columns = [];

        columns.push({
            name: 'Id',
            hidden: true,
            key: true
        });

        columns.push({
            name: 'Title',
            index: 'Title',
            width: 250
        });

        columns.push({
            name: 'ShortDescription',
            width: 250,
            sortable: false,
            hidden: true
        });

        columns.push({
            name: 'Description',
            width: 250,
            sortable: false,
            hidden: true
        });

        columns.push({
            name: 'Category.Id',
            hidden: true
        });

        columns.push({
            name: 'Category.Name',
            index: 'Category',
            width: 150
        });

        columns.push({
            name: 'Tags',
            width: 150
        });

        columns.push({
            name: 'Meta',
            width: 250,
            sortable: false
        });

        columns.push({
            name: 'UrlSlug',
            width: 200,
            sortable: false
        });

        columns.push({
            name: 'Published',
            index: 'Published',
            width: 100,
            align: 'center'
        });

        columns.push({
            name: 'PostedOn',
            index: 'PostedOn',
            width: 150,
            align: 'center',
            sorttype: 'date',
            datefmt: 'd/m/Y'
        });

        columns.push({
            name: 'Modified',
            index: 'Modified',
            width: 100,
            align: 'center',
            sorttype: 'date',
            datefmt: 'd/m/Y'
        });

        $(gridName).jqGrid({
            url: '/Admin/Posts',
            datatype: 'json',
            mtype: 'GET',

            height: 'auto',

            colNames: columnNames,
            colModel: columns,

            toppager: true,
            pager: pagerName,
            rowNumber: 10,
            rowList: [10, 20, 30],

            rownumbers: true,
            rownumWidth: 40,

            sortname: 'PostedOn',
            sortorder: 'desc',

            viewrecords: true,

            jsonReader: { repeatitems: false },

            afterInsertRow: function (rowid, rowdata, rowelem) {
                var tags = rowdata["Tags"];
                var tagStr = "";

                $.each(tags, function (i, t) {
                    if (tagStr) tagStr += ", "
                    tagStr += t.Name;
                });

                $(gridName).setRowData(rowid, { "Tags": tagStr });
            }
        });
    },

    categoriesGrid: function (gridName, pagerName) {
    },

    tagsGrid: function (gridName, pagerName) {
    }
}