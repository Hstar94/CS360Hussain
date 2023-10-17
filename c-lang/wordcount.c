#include <stdio.h>
#include <stdlib.h>
#include <string.h>

/* Generated by GPT-4 when asked this prompt:
I'm interested in a console based program that will read a text file, 
line by line, and count the number of words in the file.  It will take 
the name of the file to read on the command line and will print the 
number of words out to the command line.  Can you write this code 
in the C language?
*/
int main(int argc, char *argv[]) 
{
    FILE *file;
    char line[256];
    int wordCount = 0;

    if (argc != 2) 
    {
        printf("Usage: %s <filename>\n", argv[0]);
        return 1;
    }

    file = fopen(argv[1], "r");
    if (file == NULL) 
    {
        printf("Could not open file %s\n", argv[1]);
        return 1;
    }

    while (fgets(line, sizeof(line), file)) 
    {
        char *token = strtok(line, " \t\n");
        while (token != NULL) {
            wordCount++;
            token = strtok(NULL, " \t\n");
        }
    }

    fclose(file);

    printf("The file contains %d words.\n", wordCount);

    return 0;
}