import {
  ApplicationRef,
  ComponentFactoryResolver,
  ComponentRef,
  Injectable,
  Injector,
} from '@angular/core';
import { ErrorPopupComponent } from '../components/error-popup/error-popup.component';

@Injectable({
  providedIn: 'root',
})
export class ErrorPopupService {
  private popups: ComponentRef<ErrorPopupComponent>[] = [];

  constructor(
    private componentFactoryResolver: ComponentFactoryResolver,
    private appRef: ApplicationRef,
    private injector: Injector
  ) {}

  show(message: string, duration: number = 5000): void {
    // Create component dynamically
    const componentFactory =
      this.componentFactoryResolver.resolveComponentFactory(
        ErrorPopupComponent
      );
    const componentRef = componentFactory.create(this.injector);

    // Set component inputs
    componentRef.instance.message = message;
    componentRef.instance.duration = duration;

    // Track when component is destroyed
    componentRef.onDestroy(() => {
      this.removePopupFromDOM(componentRef);
      this.popups = this.popups.filter((p) => p !== componentRef);
    });

    // Add to DOM
    this.appRef.attachView(componentRef.hostView);
    const domElement = (componentRef.hostView as any).rootNodes[0];
    document.body.appendChild(domElement);

    // Store reference
    this.popups.push(componentRef);
  }

  private removePopupFromDOM(
    componentRef: ComponentRef<ErrorPopupComponent>
  ): void {
    this.appRef.detachView(componentRef.hostView);
    componentRef.destroy();
  }
}
