var DCS = new Array();
DCS['USA'] = [["Alabama", "AL"], ["Alaska", "AK"], [" Arizona", "AZ"], ["Arkansas", "AR"],
[" California", "CA"], ["Colorado", "CO"], ["Connecticut", "CT"], ["Delaware", "DE"]];

DCS['CAN'] = [['Alberta', 'AB'], ['British Columbia', 'BC'], ['Manitoba', 'MB'], ['New Brunswick', 'NB'],
['Newfoundland and Labrador', 'NL'], ['Nova Scotia', 'NS'], ['Northwest Territories', 'NT'],
['Nunavut', 'NU'], ['Ontario', 'ON'], ['Prince Edward Island', 'PE'], ['Quebec', 'QC'], ['Saskatchewan', 'SK'], ['Yukon', 'YT']];
DCS['MEX'] = [['Aguascalientes', 'AG'], ['Baja California', 'BC'], ['Campeche', 'CM'], ['Chihuahua', 'CH'],
['Guanajuato', 'GT'], ['Hidalgo', 'HG'], ['Guerrero', 'GR'], ['Jalisco', 'JA']];

var DSC = new Array();
DSC['USA_AL'] = ['Montgomery']; DSC['USA_AK'] = ['Juneau']; DSC['USA_AZ'] = ['Phoenix']; DSC['USA_AR'] = ['Little Rock'];
DSC['USA_CA'] = ['Sacramento']; DSC['USA_CO'] = ['Denver']; DSC['USA_CT'] = ['Hartford']; DSC['USA_DE'] = ['Dover']; 

DSC['CAN_AB'] = ['Edmonton', 'Calgary']; DSC['CAN_BC'] = ['Victoria', 'Vancouver']; DSC['CAN_MB'] = ['Winnipeg'];
DSC['CAN_NB'] = ['Fredericton', 'Moncton']; DSC['CAN_NL'] = ["St. John's"];
DSC['CAN_NS'] = ['Halifax']; DSC['CAN_NT'] = ['Yellowknife']; DSC['CAN_NU'] = ['Iqaluit'];
DSC['CAN_ON'] = ['Toronto']; DSC['CAN_PE'] = ['Charlottetown']; DSC['CAN_QC'] = ['Quebec City', 'Montreal'];
DSC['CAN_SK'] = ['Regina', 'Saskatoon']; DSC['CAN_YT'] = ['Whitehorse'];

DSC['MEX_AG'] = ['Aguascalientes']; DSC['MEX_BC'] = ['Mexicali', 'Tijuana']; DSC['MEX_CM'] = ['San Francisco de Campeche'];
DSC['MEX_CH'] = ['Chihuahua', 'Ciudad Juárez']; DSC['MEX_GT'] = ['Guanajuato', 'León']; DSC['MEX_HG'] = ['Pachuca'];
DSC['MEX_GR'] = ['Chilpancingo de los Bravo', 'Acapulco']; DSC['MEX_JA'] = ['Guadalajara'];
//-------------------------------------------------

$(document).ready(function (event) {

    $("#Country").change(function (event) {
        event.preventDefault();
        $("#Province").empty();
        $("#City").empty();
        $("#City").append('<option value="City">--Please Select a City--</option>');
        var country = $("#Country").val();
        var states = DCS[country];
        GetStates(states);
    });

    return false;
});

$(document).ready(function (event) {

    $("#Province").change(function (event) {
        event.preventDefault();
        $("#City").empty();
        var country = $("#Country").val();
        var state = $("#Province").val();
        var c_s = country+'_'+ state;
        var cities = DSC[c_s];
        GetCities(cities);
    });

    return false;
});

function GetStates(states) {
    $("#Province").append('<option value="State">--Please Select a State--</option>');
    for (var i = 0; i < states.length; i++) {
        $("#Province").append('<option value="' + states[i][1] + '">' + states[i][0] + '</option>');
    }
}

function GetCities(cities) {
    $("#City").append('<option value="City">--Please Select a City--</option>');
    for (var i = 0; i < cities.length; i++)
    {
            $("#City").append('<option value="' + cities[i] + '">' + cities[i] + '</option>');
    }
}