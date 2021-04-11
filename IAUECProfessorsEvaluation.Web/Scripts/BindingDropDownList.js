$(function () {
    alert('f');
function InitialDropDownList() {

    $('#ColList').multiselect({

        onInitialized: function () {
            $.getJSON('@Url.Action("GetColleges","Reports")', null, function (data) {
                $.each(data, function () {
                    $('#ColList').append('<option value=' +
                        this.Id + ' selected' + '>' + this.Name + '</option>');
                });
                $('#ColList').multiselect('rebuild');

            });
        },
        onChange: function (element, checked) {
            var colleges = $('#ColList option:selected');
            var selectedColleges = new Array();
            $(colleges).each(function (index, college) {

                var v = $(this).val();

                selectedColleges.push(v);

            });

            $('option', $('#GrpList')).remove();
            var jsonColleges = JSON.stringify(selectedColleges);
            $('#GrpList').multiselect('rebuild');

            $.getJSON('@Url.Action("GetGroups","Reports")',
                { collegesList: jsonColleges },
                function (data) {
                    $.each(data,
                        function () {
                            $('#GrpList').append('<option value=' +
                                this.Id +
                                ' selected' +
                                '>' +
                                this.Name +
                                '</option>');
                        });
                    $('#GrpList').multiselect('rebuild');

                });
        },
        maxWidth: 500,
        includeSelectAllOption: true,
        enableFiltering: true,
        filterPlaceholder: 'جستجو',
        allSelectedText: "تمامی موراد انتخاب گردید",
        buttonText: function (options, select) {
            if (options.length === 0) {
                return 'هیچ موردی انتخاب نگردید';
            }
            var countOfOption = $('#ColList > option').length;

            if (options.length > 0 && options.length < countOfOption) {
                return (options.length.toString() + ' مورد انتخاب گردید');
            } else {
                return 'همه موراد انتخاب گردید';
            }

        },
        selectAllText: 'انتخاب تمامی موارد',
        selectAllValue: 0,
        disableIfEmpty: true,
        buttonWidth: '400px'
    });
    $('#GrpList').multiselect({
        maxWidth: 500,
        includeSelectAllOption: true,
        enableFiltering: true,
        filterPlaceholder: 'جستجو',
        allSelectedText: "تمامی موراد انتخاب گردید",
        buttonText: function (options, select) {
            if (options.length === 0) {
                return 'هیچ موردی انتخاب نگردید';
            }
            var countOfOption = $('#GrpList > option').length;

            if (options.length > 0 && options.length < countOfOption) {
                return (options.length.toString() + ' مورد انتخاب گردید');
            } else {
                return 'همه موراد انتخاب گردید';
            }

        },
        selectAllText: 'انتخاب تمامی موارد',
        selectAllValue: 0,
        disableIfEmpty: true,
        buttonWidth: '400px'
    });
    $('#proList').multiselect({

        onDropdownShown: function () {
            var groups = $('#GrpList option:selected');
            var selectedGroups = new Array();
            $(groups).each(function (index, college) {

                var g = $(this).val();

                selectedGroups.push(g);

            });

            $('option', $('#proList')).remove();
            var jsonGroups = JSON.stringify(selectedGroups);


            $.getJSON('@Url.Action("GetProfessores","Reports")',
                { groupList: jsonGroups },
                function (data) {
                    $.each(data,
                        function () {
                            $('#proList').append('<option value=' +
                                this.Id +
                                ' selected' +
                                '>' +
                                this.Name +
                                '</option>');
                        });
                    $('#proList').multiselect('rebuild');

                });
        },
        maxWidth: 500,
        includeSelectAllOption: true,
        enableFiltering: true,
        filterPlaceholder: 'جستجو',
        allSelectedText: "تمامی موراد انتخاب گردید",
        buttonText: function (options, select) {
            if (options.length === 0) {
                return 'هیچ موردی انتخاب نگردید';
            }
            var countOfOption = $('#ColList > option').length;

            if (options.length > 0 && options.length < countOfOption) {
                return (options.length.toString() + ' مورد انتخاب گردید');
            } else {
                return 'همه موراد انتخاب گردید';
            }

        },
        selectAllText: 'انتخاب تمامی موارد',
        selectAllValue: 0,

        buttonWidth: '400px'
    });


    }


    InitialDropDownList();
});



