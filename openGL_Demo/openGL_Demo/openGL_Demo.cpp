// openGL_Demo.cpp : �������̨Ӧ�ó������ڵ㡣
//

#include "stdafx.h"
#include <gl/glut.h>
void myDisplay(void){
	glClear(GL_COLOR_BUFFER_BIT);
	//glRectf(-0.5f, -0.5f, 0.5f, 0.5f);
	glRotatef(67,1,2,3);
	glFlush();
}
int _tmain(int argc, _TCHAR* argv[])
{
	glutInit(&argc,(char**)argv); 
    glutInitDisplayMode(GLUT_RGB | GLUT_SINGLE);
	glutInitWindowPosition(100, 100);
	glutInitWindowSize(400, 400);
    glutCreateWindow("��һ��OpenGL����");     
	glutDisplayFunc(&myDisplay);     
	glutMainLoop();
	return 0;
}

