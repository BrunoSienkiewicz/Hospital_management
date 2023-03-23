CREATE TABLE Users (
    UserId INT PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(50) NOT NULL,
    UserType NVARCHAR(20) NOT NULL
);

CREATE TABLE Patients (
    PatientId INT PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(50) NOT NULL UNIQUE,
    PhoneNumber NVARCHAR(20) NOT NULL UNIQUE,
    Address NVARCHAR(100) NOT NULL,
    Pesel NVARCHAR(11) NOT NULL UNIQUE,
    DateofBirth DATETIME NOT NULL,
    UserId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE Doctors (
	DoctorId INT PRIMARY KEY,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	Email NVARCHAR(50) NOT NULL UNIQUE,
	Specialty NVARCHAR(50) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
	UserId INT NOT NULL,
	FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE Appointments (
    AppointmentId INT PRIMARY KEY,
    DoctorId INT NOT NULL,
    PatientId INT NOT NULL,
    AppointmentDate DATETIME NOT NULL,
    Notes NVARCHAR(200) NOT NULL,
    FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId),
    FOREIGN KEY (PatientId) REFERENCES Patients(PatientId)
);

CREATE TABLE Prescriptions (
    PrescriptionId INT PRIMARY KEY,
    DoctorId INT NOT NULL,
    PatientId INT NOT NULL,
    AppointmentId INT NOT NULL,
    Medication NVARCHAR(100) NOT NULL,
    Dosage NVARCHAR(20) NOT NULL,
    Frequency NVARCHAR(20) NOT NULL,
    FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId),
    FOREIGN KEY (PatientId) REFERENCES Patients(PatientId),
    FOREIGN KEY (AppointmentId) REFERENCES Appointments(AppointmentId)
);