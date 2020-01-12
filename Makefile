NAME = N-puzzle
OBJ = obj/


all:		$(NAME)

$(NAME):	
	@dotnet publish -c Release -r osx.10.13-x64 --self-contained true
	@echo "Build complited"

clean:
	@rm -rf $(OBJ)
	@echo "obj deleted"
	
fclean:		clean
	@rm -rf bin/
	@echo "bin deleted"
	
re:			fclean	all

.PHONY: clean all fclean re
