NAME = N-puzzle
OBJ = obj


all:		$(NAME)

$(NAME):	
	dotnet publish -c Release -r osx.10.13-x64 --self-contained true

clean:
	@/bin/rm -f $(OBJ)
	
fclean:		clean
	
	/bin/rm -f bin
	
re:			fclean	all

.PHONY: clean all fclean re
