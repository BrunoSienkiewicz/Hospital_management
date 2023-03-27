INSERT INTO Users (UserId, Username, Password, UserType)
VALUES (1, 'johndoe', 'password123', 'user'),
       (2, 'janesmith', 'password321', 'user'),
       (3, 'bobjohnson', '123password', 'user'),
	   (4, 'davidlee', 'password123', 'doctor'),
       (5, 'emilychen', 'password321', 'doctor'),
       (6, 'alexnguyen', '123password', 'doctor');

INSERT INTO Patients (PatientId, FirstName, LastName, Email, PhoneNumber, Address, Pesel, DateOfBirth, UserId)
VALUES (1, 'John', 'Doe', 'johndoe@example.com', '123456789', '123 Main St', '12345678901', '1990-01-01', 1),
       (2, 'Jane', 'Smith', 'janesmith@example.com', '987654321', '456 Oak St', '23456789012', '1985-05-10', 2),
       (3, 'Bob', 'Johnson', 'bobjohnson@example.com', '5551112222', '789 Maple Ave', '34567890123', '1972-12-15', 3);

INSERT INTO Doctors (DoctorId, FirstName, LastName, Email, Specialty, PhoneNumber, UserId)
VALUES (1, 'David', 'Lee', 'davidlee@example.com', 'Cardiology', '4443332222', 4),
       (2, 'Emily', 'Chen', 'emilychen@example.com', 'Pediatrics', '7778889999', 5),
       (3, 'Alex', 'Nguyen', 'alexnguyen@example.com', 'Neurology', '5556667777', 6);

INSERT INTO Appointments (AppointmentId, DoctorId, PatientId, AppointmentDate, Notes)
VALUES (1, 1, 1, '2023-03-21 10:00:00', 'Annual checkup'),
       (2, 2, 2, '2023-04-05 14:30:00', 'Vaccination'),
       (3, 3, 3, '2023-04-10 11:00:00', 'Headache');

INSERT INTO Prescriptions (PrescriptionId, DoctorId, PatientId, AppointmentId, Medication, Dosage, Frequency)
VALUES (1, 1, 1, 1, 'Lipitor', '20mg', 'Once daily'),
       (2, 2, 2, 2, 'Fluoxetine', '10mg', 'Once daily'),
       (3, 3, 3, 3, 'Ibuprofen', '400mg', 'Twice daily');
