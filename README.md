Usega
- turn on cmd
- dbexport <connectionstring> <select query> [-f:filename] [-server:<SqlServer>] [-format:<csv|tsql>] [-compress] [-adt]

example
	DBExport "Server=.;Database=Test" "SELECT * FROM table" "-f:filename" "-server:SqlServer" "-format:csv" "-compress" "-adt"
