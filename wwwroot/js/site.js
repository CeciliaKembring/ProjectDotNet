
// Intervall av tid enligt Frisörernas öppettider
document.getElementById("Time").addEventListener("change", function() {
    var selectedTime = this.value;
    var minTime = "10:00";
    var maxTime = "16:00";
    
    if (selectedTime < minTime || selectedTime > maxTime) {
        alert("Vänligen välj tid mellan 10:00 och 16:00.");
        this.value = ""; 
    }
});

// Spärra för att inte kunna boka innan dagens datum
document.addEventListener("DOMContentLoaded", function () {
  
    var dateInput = document.getElementById("Date");

    // Hämta dagens datum och formatera det till YYYY-MM-DD
    var today = new Date().toISOString().split("T")[0];

    // Sätt min-attributet för datum-inputfältet till dagens datum
    dateInput.min = today;

    dateInput.addEventListener("change", function () {
        var selectedDate = this.value;

        // Om det valda datumet är tidigare än dagens datum, återställ värdet till dagens datum
        if (selectedDate < today) {
            alert("Du kan inte boka ett datum som redan har passerat.");
            this.value = today;
        }
    });
});
