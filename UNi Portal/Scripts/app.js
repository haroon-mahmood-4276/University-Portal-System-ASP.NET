$(document).ready(function () {

    $("#mySTDBtn").click(function () {
        $("#mySTDModal").modal();
        GetCountries();
        GetSchools();
    });

    $("#myTCHRBtn").click(function () {
        $("#myTCHRModal").modal();
        GetCountries();
        GetSchools();
    });

    $("#myProgramBtn").click(function () {
        $("#myProgramModal").modal();
    });

    $("#mySchoolBtn").click(function () {
        $("#mySchoolModal").modal();
    });

    $("#cbProgram").ready(function () {
        GetPrograms();
    });

    $("#cbExamType").ready(function () {
        GetExams();
    });

    function GetPrograms() {

        var Data = "";

        $.ajax({
            type: "get",
            url: '/api/programs',
            dataType: 'json',
            success: function (response) {
                Data += '<option value="000" selected>Select</option>';
                for (let index = 0; index < response.length; index++) {
                    Data += '<option value="' + response[index].PRG_PCode + '">' + response[index].PRG_ProgramName + '</option>';
                }
                $('#cbProgram').html(Data);
            }
        });
    };

    function GetExams() {

        var Data = "";

        $.ajax({
            type: "get",
            url: '/api/exams',
            dataType: 'json',
            success: function (response) {
                Data += '<option value="00" selected>Select</option>';
                for (let index = 0; index < response.length; index++) {
                    Data += '<option value="' + response[index].EXAM_ID + '">' + response[index].EXAM_Name + '</option>';
                }
                $('#cbExamType').html(Data);
            }
        });
    };


    function GetCountries() {

        var Data = "";

        $.ajax({
            type: "get",
            url: '/api/countries',
            dataType: 'json',
            success: function (response) {
                Data += '<option value="000" selected>Select</option>';
                for (let index = 0; index < response.length; index++) {
                    Data += '<option value="' + response[index].CC_CntryCode + '">' + response[index].CC_CntryName + '</option>';
                }
                $('#cbCountry').html(Data);
            }
        });
    };

    function GetSchools() {

        var Data = "";

        $.ajax({
            type: "get",
            url: '/api/schools',
            dataType: 'json',
            success: function (response) {
                Data += '<option value="000" selected>Select</option>';
                for (let index = 0; index < response.length; index++) {
                    Data += '<option value="' + response[index].SCL_SchoolCode + '">' + response[index].SCL_SchoolName + ' ( ' + response[index].SCL_SchoolAbb + ' )' + '</option>';
                }
                $('#cbSchool').html(Data);
            }
        });
    };


    $('#cbCountry').on('change', function () {

        var CountryId = $(this).val();

        var Data = "";

        $.ajax({
            type: "get",
            url: '/api/countries/' + CountryId + '/cities',
            dataType: 'json',
            success: function (response) {
                Data += '<option value="000" selected>Select</option>';
                for (let index = 0; index < response.length; index++) {
                    Data += '<option value="' + response[index].CC_CityCode + '">' + response[index].CC_CityName + '</option>';
                }
                $('#cbCity').html(Data);
            }
        });
    });

    $('#cbSchool').on('change', function () {

        var SchoolId = $(this).val();

        var Data = "";

        $.ajax({
            type: "get",
            url: '/api/schools/' + SchoolId + '/programs',
            dataType: 'json',
            success: function (response) {
                Data += '<option value="000" selected>Select</option>';
                for (let index = 0; index < response.length; index++) {
                    Data += '<option value="' + response[index].PRG_PCode + '">' + response[index].PRG_ProgramName + '</option>';
                }
                $('#cbProgram').html(Data);
            }
        });
    });

    $('#cbProgram').on('change', function () {

        var ProgramId = $(this).val();

        var Data = "";

        $.ajax({
            type: "get",
            url: '/api/programs/' + ProgramId + '/sections',
            dataType: 'json',
            success: function (response) {
                Data += '<option value="00" selected>Select</option>';
                for (let index = 0; index < response.length; index++) {
                    Data += '<option value="' + response[index].PRG_SCode + '">' + response[index].PRG_SectionName + '</option>';
                }
                $('#cbSection').html(Data);
            }
        });
    });

    $("#myInput").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#myTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });


    $('#inputGroupFile02').on('change', function () {
        //get the file name
        var fileName = $(this).val();
        //replace the "Choose a file" label
        $(this).next('.custom-file-label').html(fileName);
    })

    //function ShowSTDMARKS(PID, SID) {
    //    if (PID == 0 && SID == 0) {
    //        var ProgramID = PID;
    //        var SectionID = SID;
    //    } else {
    //        var ProgramID = $(this).val();
    //        var SectionID = $(this).val();
    //        var TotalMarks = $(this).vabi::before {
    //            display: inline-block;
    //            content: "";
    //            background-image: url("data:image/svg+xml,<svg viewBox='0 0 16 16' fill='%23333' xmlns='http://www.w3.org/2000/svg'><path fill-rule='evenodd' d='M8 9.5a1.5 1.5 0 1 0 0-3 1.5 1.5 0 0 0 0 3z' clip-rule='evenodd'/></svg>");
    //            backgroundl();
    //        }
    //        console.log(ProgramID + ' ' + SectionID);

    //        var xhttp = new XMLHttpRequest();
    //        xhttp.onreadystatechange = function () {
    //            if (this.readyState == 4 && this.status == 200) {
    //                document.getElementById("STDMARKSList").innerHTML = this.responseText;
    //            }
    //        };
    //        xhttp.open("POST", "STDList.php", true);
    //        xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    //        xhttp.send("ProgramID=" + ProgramID + "&SectionID=" + SectionID + "&WorkAction=STDMarks&TotalMarks=" + TotalMarks);
    //    }

});