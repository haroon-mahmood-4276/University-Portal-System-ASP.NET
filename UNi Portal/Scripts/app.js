$(document).ready(function () {


    $('#STD_CCCntryCode').on('change', function () {

        var CountryId = $(this).val();

        var Data = "";

        $.ajax({
            type: "get",
            url: '/api/countries/' + CountryId,
            dataType: 'json',
            success: function (response) {
                response = JSON.parse(response);
                Data += '<option value selected>Select</option>';
                for (let index = 0; index < response.length; index++) {
                    Data += '<option value="' + response[index].CC_CityCode + '">' + response[index].CC_CityName + '</option>';
                }
                $('#STD_CCCityCode').html(Data);
            }
        });
    });

    $('#PRG_SCLSchoolCode').on('change', function () {

        var SchoolId = $(this).val();

        var Data = "";

        $.ajax({
            type: "get",
            url: '/api/schools/' + SchoolId,
            dataType: 'json',
            success: function (response) {
                response = JSON.parse(response);
                Data += '<option value selected>Select</option>';
                for (let index = 0; index < response.length; index++) {
                    Data += '<option value="' + response[index].PRG_PCode + '">' + response[index].PRG_ProgramName + '</option>';
                }
                $('#PRG_PCode').html(Data);
            }
        });
    });

    $('#PRG_PCode').on('change', function () {

        var ProgramId = $(this).val();
        var SchoolId = $('#PRG_SCLSchoolCode').val();
        var Data = "";

        $.ajax({
            type: "get",
            url: '/api/programs/' + SchoolId + '/' + ProgramId,
            dataType: 'json',
            success: function (response) {
                response = JSON.parse(response);
                Data += '<option value selected>Select</option>';
                for (let index = 0; index < response.length; index++) {
                    Data += '<option value="' + response[index].PRG_SCode + '">' + response[index].PRG_SectionName + '</option>';
                }
                $('#PRG_SCode').html(Data);
            }
        });
    });

    $('#createMarksSheet').on('click', function () {

        var SchoolId = $('#PRG_SCLSchoolCode').val();
        var ProgramId = $('#PRG_PCode').val();
        var SectionId = $('#PRG_SCode').val();
        var Data = "";
        var Sr = 1;
        $.ajax({
            type: "get",
            url: '/api/teacher/students/get/' + SchoolId + '/' + ProgramId + '/' + SectionId,
            dataType: 'json',
            success: function (response) {
                response = JSON.parse(response);
                $("table tbody").empty();
                for (let index = 0; index < response.length; index++) {
                    Data += '<tr>' +
                         '<td class="align-middle">' + Sr + '</td>' +
                         '<td class="align-middle">' + response[index].STD_RollNo + '</td>' +
                         '<td class="align-middle">' + response[index].STD_FirstName + ' ' + response[index].STD_LastName + '</td>' +
                         '<td class="align-middle">' + response[index].PRG_ProgramName + ' - ' + response[index].PRG_SectionName + '</td>' +
                         '<td class="align-middle">' + response[index].SCL_SchoolName + ' - ' + response[index].SCL_SchoolAbb + '</td>' +
                         '<td class="text-center align-middle">' +
                         '<div class="custom-control custom-checkbox">' +
                         '<input type="number" class="form-control" name="' + response[index].STD_RollNo + 'OM" id="' + response[index].STD_RollNo + 'OM" min="0" max="' + $('#TotalMarks').val() + '" value="0" required>' +
                         '</div></td>' +
                     '</tr>';
                    Sr++;
                }
                $("table tbody").append(Data);
            }
        });
    });

    $('#createAttendance').on('click', function () {

        var SchoolId = $('#PRG_SCLSchoolCode').val();
        var ProgramId = $('#PRG_PCode').val();
        var SectionId = $('#PRG_SCode').val();
        var Data = "";
        var Sr = 1;
        $.ajax({
            type: "get",
            url: '/api/teacher/students/get/' + SchoolId + '/' + ProgramId + '/' + SectionId,
            dataType: 'json',
            success: function (response) {
                response = JSON.parse(response);
                $("table tbody").empty();
                for (let index = 0; index < response.length; index++) {
                    Data += '<tr>' +
                         '<td class="align-middle">' + Sr + '</td>' +
                         '<td class="align-middle">' + response[index].STD_RollNo + '</td>' +
                         '<td class="align-middle">' + response[index].STD_FirstName + ' ' + response[index].STD_LastName + '</td>' +
                         '<td class="align-middle">' + response[index].PRG_ProgramName + ' - ' + response[index].PRG_SectionName + '</td>' +
                         '<td class="align-middle">' + response[index].SCL_SchoolName + ' - ' + response[index].SCL_SchoolAbb + '</td>' +
                         '<td class="text-center align-middle">' +
                         '<div class="custom-control custom-checkbox">' +
                         '<input type="checkbox" class="custom-control-input" id="' + response[index].STD_RollNo + 'Chk" name="' + response[index].STD_RollNo + 'Chk" checked>' +
                         '<label class="custom-control-label" for="' + response[index].STD_RollNo + 'Chk">Present</label>' +
                         '</div></td>' +
                     '</tr>';
                    Sr++;
                }
                $("table tbody").append(Data);
            }
        });
    });

    $("#resetMarksSheet").on('click', function () {
        $("table tbody").empty();
    });

    $("#myInput").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#myTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });


});