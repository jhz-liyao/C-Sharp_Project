// queue_test.cpp : 定义控制台应用程序的入口点。
//

#include "stdafx.h"

#define QUEUE_MAXNUM 10
typedef struct{
	uint16_t id;
	
	uint8_t* data8;
	uint16_t* data16;
	uint32_t* data32;
	
	uint8_t single_size;
	uint16_t count;
	
	uint16_t start;
	uint16_t end;
	uint16_t is_full;
}QUEUE_T;

QUEUE_T queue_list[QUEUE_MAXNUM] = {0};
uint8_t queue_index = 0;



QUEUE_T* queue_init(void* _data,uint8_t _single_size ,uint16_t _count){
	if(queue_index+1 == QUEUE_MAXNUM)
		return NULL;
	QUEUE_T* queue = &queue_list[queue_index];
	queue->single_size = _single_size;
	queue->count = _count;
	
	queue->start = 0;
	queue->end = 0;
	queue->is_full = 0;
	
	switch(_single_size){
		case 1:queue->data8 = (uint8_t*)_data;	break;
		case 2:queue->data16 = (uint16_t*)_data;	break;
		case 4:queue->data32 = (uint32_t*)_data;	break;
		default:return NULL;
	}  
	queue_index++;
	return queue;
}

uint8_t queue_add(QUEUE_T* queue,uint8_t _data8, uint16_t _data16, uint32_t _data32){
		switch(queue->single_size){
			case 1:*(queue->data8 + queue->start) = _data8;	break;
			case 2:*(queue->data16 + queue->start) = _data16;	break;
			case 4:*(queue->data32 + queue->start) = _data32;	break;
			default:return -1;
		}  
		return 0;
}


int _tmain(int argc, _TCHAR* argv[])
{
	printf("123\n");
	while(1);
	return 0;
}

