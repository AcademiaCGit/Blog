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
            width: 250,
            editable: true,
            editoptions: {
                size: 43,
                maxlength: 500
            },
            editrules: {
                required: true
            }
        });

        columns.push({
            name: 'ShortDescription',
            width: 250,
            sortable: false,
            hidden: true,
            editable: true,
            edittype: 'textarea',
            editoptions: {
                rows: "10",
                cols: "100"
            },
            editrules: {
                custom: true,

                custom_func: function (val, colname) {
                    val = tinyMCE.get("ShortDescription").getContent();
                    if (val) return [true, ""];
                    return [false, colname + ": Câmpul este obligatoriu"];
                },

                edithidden: true
            }
        });

        columns.push({
            name: 'Description',
            width: 250,
            sortable: false,
            hidden: true,
            editable: true,
            edittype: 'textarea',
            editoptions: {
                rows: "40",
                cols: "100"
            },
            editrules: {
                custom: true,

                custom_func: function (val, colname) {
                    val = tinyMCE.get("Description").getContent();
                    if (val) return [true, ""];
                    return [false, colname + ": Câmpul este obligatoriu"];
                },

                edithidden: true
            }
        });

        columns.push({
            name: 'Category.Id',
            hidden: true,
            editable: true,
            edittype: 'select',
            editoptions: {
                style: 'width:250px;',
                dataUrl: '/Admin/GetCategoriesHtml'
            },
            editrules: {
                required: true,
                edithidden: true
            }
        });

        columns.push({
            name: 'Category.Name',
            index: 'Category',
            width: 150
        });

        columns.push({
            name: 'Tags',
            width: 150,
            editable: true,
            edittype: 'select',
            editoptions: {
                style: 'width:250px;',
                dataUrl: '/Admin/GetTagsHtml',
                multiple: true
            },
            editrules: {
                required: true
            }
        });

        columns.push({
            name: 'Meta',
            width: 250,
            sortable: false,
            editable: true,
            edittype: 'textarea',
            editoptions: {
                rows: "2",
                cols: "40",
                maxlength: 1000
            },
            editrules: {
                required: true
            }
        });

        columns.push({
            name: 'UrlSlug',
            width: 200,
            sortable: false,
            editable: true,
            editoptions: {
                size: 43,
                maxlength: 200
            },
            editrules: {
                required: true
            }
        });

        columns.push({
            name: 'Published',
            index: 'Published',
            width: 100,
            align: 'center',
            editable: true,
            edittype: 'checkbox',
            editoptions: {
                value: "true:false",
                defaultValue: 'false'
            }
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
            toppager: true,

            colNames: columnNames,
            colModel: columns,

            pager: pagerName,
            rownumbers: true,
            rownumWidth: 40,
            rowNum: 10,
            rowList: [10, 20, 30],

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

        var afterShowForm = function (form) {
            tinyMCE.execCommand('mceAddControl', false, "ShortDescription");
            tinyMCE.execCommand('mceAddControl', false, "Description");
        }

        var onClose = function (form) {
            tinyMCE.execCommand('mceRemoveControl', false, "ShortDescription");
            tinyMCE.execCommand('mceRemoveControl', false, "Description");
        }

        var beforeSubmitHandler = function (postdata, form) {
            var selRowData = $(gridName).getRowData($(gridName).getGridParam('selrow'));
            if (selRowData["PostedOn"])
                postdata.PostedOn = selRowData["PostedOn"];
            postdata.ShortDescription = tinyMCE.get("ShortDescription").getContent();
            postdata.Description = tinyMCE.get("Description").getContent();

            return [true];
        }

        var addOptions = {
            url: '/Admin/AddPost',
            addCaption: 'Adaugă Postare',
            processData: "Se salvează...",
            width: 900,
            closeAfterAdd: true,
            closeOnEscape: true,
            afterShowForm: afterShowForm,
            onClose: onClose,
            afterSubmit: AcademiaCerului.GridManager.afterSubmitHandler,
            beforeSubmit: beforeSubmitHandler
        }

        var editOptions = {
            url: '/Admin/EditPost',
            addCaption: 'Editează Postarea',
            processData: "Se salvează...",
            width: 900,
            closeAfterEdit: true,
            closeOnEscape: true,
            afterShowForm: afterShowForm,
            onClose: onClose,
            afterSubmit: AcademiaCerului.GridManager.afterSubmitHandler,
            beforeSubmit: beforeSubmitHandler
        }

        var deleteOptions = {
            url: '/Admin/DeletePost',
            caption: 'Șterge Postarea',
            processData: "Se salvează...",
            msg: "Sunteți sigur că doriți să ștergeți postarea ?",
            closeOnEscape: true,
            afterSubmit: AcademiaCerului.GridManager.afterSubmitHandler
        }

        $(gridName).navGrid(pagerName,
                    {
                        cloneToTop: true,
                        search: false
                    },
                    editOptions, addOptions, deleteOptions);
    },

    categoriesGrid: function (gridName, pagerName) {
        var columnNames = ['Id', 'Name', 'UrlSlug', 'Description'];
        var columns = [];

        columns.push({
            name: 'Id',
            index: 'Id',
            hidden: true,
            sorttype: 'int',
            key: true,
            editable: false,
            editoptions: {
                readonly: true
            }
        });

        columns.push({
            name: 'Name',
            index: 'Name',
            width: 200,
            editable: true,
            edittype: 'text',
            editoptions: {
                size: 30,
                maxlength: 50
            },
            editrules: {
                required: true
            }
        });

        columns.push({
            name: 'UrlSlug',
            index: 'UrlSlug',
            width: 200,
            editable: true,
            edittype: 'text',
            sortable: false,
            editoptions: {
                size: 30,
                maxlength: 50
            },
            editrules: {
                required: true
            }
        });

        columns.push({
            name: 'Description',
            index: 'Description',
            width: 200,
            editable: true,
            edittype: 'textarea',
            sortable: false,
            editoptions: {
                rows: "4",
                columns: "28"
            }
        });

        $(gridName).jqGrid({
            url: '/Admin/Categories',
            datatype: 'json',
            mtype: 'GET',
            height: 'auto',
            toppager: true,
            colNames: columnNames,
            colModel: columns,
            pager: pagerName,
            rownumbers: true,
            rownumWidth: 40,
            rowNum: 500,
            sortname: 'Name',
            loadonce: true,
            jsonReader: {
                repeatitems: false
            }
        });

        $(gridName).jqGrid('navGrid', pagerName,
            {
                cloneToTop: true,
                search: false
            },
            {}, {}, {});
    },

    tagsGrid: function (gridName, pagerName) {        
    },

    afterSubmitHandler: function (response, postdata) {
        var json = $.parseJSON(response.responseText);

        if (json) return [json.success, json.message, json.id];

        return [false, "A apărut o eroare pe server", null];
    }
}