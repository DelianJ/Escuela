Create Database DBEscuela;

Use DBEscuela;

Create Table Estudiantes(
	ID_Estudiante int identity(1,1) Not Null,
	Nombre_Estudiante varchar(50)
	Primary Key (ID_Estudiante)
);

Create Table Profesores(
	ID_Profesor int identity(1,1) Not Null,
	Nombre_Profesor varchar(50)
	Primary Key (ID_Profesor)
);

Create Table Materias(
	ID_Materia int identity(1,1) Not Null,
	Nomnbre_Materia varchar(20)
	Primary Key (ID_Materia)
);

Create Table Materias_Estudiantes(
	ID_MateriasEstudiantes int identity(1,1) Not Null,
	ID_Estudiante int Not Null,
	ID_Materia int Not Null,
	Calificacion decimal(3,1),
	Primary Key (ID_MateriasEstudiantes),
	FOREIGN KEY (ID_Estudiante) REFERENCES Estudiantes(ID_Estudiante),
	FOREIGN KEY (ID_Materia) REFERENCES Materias(ID_Materia)
);

Create Table Materias_Profesores(
	ID_Profesor int Not Null,
	ID_Materia int unique Not Null,
	FOREIGN KEY (ID_Profesor) REFERENCES Profesores(ID_Profesor),
	FOREIGN KEY (ID_Materia) REFERENCES Materias(ID_Materia)
);

Insert Into Estudiantes (Nombre_Estudiante) Values 
  ('Evan Gutierrez'),
  ('Madaline Cook'),
  ('Hilel Ochoa'),
  ('George Jarvis'),
  ('Kerry Mayer');

Insert Into Profesores Values
  ('Jorden Florencia'),
  ('Salvador Lopez'),
  ('April Blanco'),
  ('Jason Castillo'),
  ('Thane Maximiliano');

Insert Into Materias Values
  ('Español'),
  ('Matematicas'),
  ('Geografia'),
  ('Historia'),
  ('Biologia');

Insert Into Materias_Estudiantes (ID_Estudiante, ID_Materia, Calificacion) Values
	(1, 1, 7),
	(1, 3, 8),
	(1, 5, 9),
	(2, 1, 6),
	(2, 2, 7),
	(2, 3, 6),
	(3, 1, 8),
	(3, 2, 6.5),
	(3, 5, 6),
	(4, 1, 8),
	(4, 3, 8),
	(4, 5, 8),
	(5, 3, 7),
	(5, 4, 6),
	(5, 5, 7);

Insert Into Materias_Profesores(ID_Profesor, ID_Materia) Values
	(1, 1),
	(1, 2),
	(2, 5),
	(3, 4),
	(4, 3);

Select * from Materias_Profesores;