import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UtilService {
  constructor() {}

  getReadableDate(date: any): string {
    if (!date) return 'Invalid Date';

    // Convert string date to Date object if necessary
    const parsedDate = date instanceof Date ? date : new Date(date);

    if (isNaN(parsedDate.getTime())) return 'Invalid Date';

    return (
      parsedDate.getDate().toString().padStart(2, '0') +
      '.' +
      (parsedDate.getMonth() + 1).toString().padStart(2, '0') + // Months are 0-based
      '.' +
      parsedDate.getFullYear()
    );
  }

  getReadableDateAndTime(date: any): string {
    if (!date) return 'Invalid Date';

    // Convert string date to Date object if necessary
    const parsedDate = date instanceof Date ? date : new Date(date);

    if (isNaN(parsedDate.getTime())) return 'Invalid Date';

    return (
      parsedDate.getDate().toString().padStart(2, '0') +
      '.' +
      (parsedDate.getMonth() + 1).toString().padStart(2, '0') + // Months are 0-based
      '.' +
      parsedDate.getFullYear() +
      ' ' +
      parsedDate.getHours().toString().padStart(2, '0') +
      ':' +
      parsedDate.getMinutes().toString().padStart(2, '0') +
      ':' +
      parsedDate.getSeconds().toString().padStart(2, '0')
    );
  }
}
