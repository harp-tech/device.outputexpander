#ifndef _PAA5100JE_H_
#define _PAA5100JE_H_
#include "cpu.h"


#define PRODUCT_ID_REG_ADD 0x00
#define MOTION_REG_ADD 0x02
#define DELTA_X_L_REG_ADD 0x3
#define DELTA_X_H_REG_ADD 0x4
#define DELTA_Y_L_REG_ADD 0x5
#define DELTA_Y_H_REG_ADD 0x6
#define MOTION_BURST_REG_ADD 0x16
#define POWER_UP_RESET_REG_ADD 0x3A
#define INVERSE_PRODUCT_ID_REG_ADD 0x5F


typedef struct Motion {
	uint8_t dummy[2];
	int16_t deltaA;
	int16_t deltaB;
	uint8_t Squal;
} Motion;


bool optical_tracking_initialize(void);
void performanceOptimizationRoutine(void);

void optical_tracking_read_motion (Motion motion[]);

uint8_t optical_tracking_read_register(uint8_t address);
void optical_tracking_write_register(uint8_t address, uint8_t byte);


#endif /* _SPI_H_ */