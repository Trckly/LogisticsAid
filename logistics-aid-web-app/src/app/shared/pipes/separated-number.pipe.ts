import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'separatedNumber',
})
export class SeparatedNumberPipe implements PipeTransform {
  transform(value: number): string {
    return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ' ');
  }
}
