NAME = N-puzzle
OBJ = obj


all:		$(NAME)

$(NAME):	
	dotnet build -c Release
	dotnet run

clean:
	@/bin/rm -f $(OBJ)
	
fclean:		clean
	
	/bin/rm -f bin
	
re:			fclean	all

.PHONY: clean all fclean re
