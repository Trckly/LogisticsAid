import {
  ApplicationRef,
  ComponentFactoryResolver,
  ComponentRef,
  Injectable,
  Injector,
  OnDestroy,
  OnInit,
} from '@angular/core';
import { SuccessPopupComponent } from '../components/success-popup/success-popup.component';

@Injectable({
  providedIn: 'root',
})
export class SuccessPopupService {
  private popups: ComponentRef<SuccessPopupComponent>[] = [];

  constructor(
    private componentFactoryResolver: ComponentFactoryResolver,
    private appRef: ApplicationRef,
    private injector: Injector
  ) {}

  show(message: string, duration: number = 5000): void {
    // Create component dynamically
    const componentFactory =
      this.componentFactoryResolver.resolveComponentFactory(
        SuccessPopupComponent
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
    componentRef: ComponentRef<SuccessPopupComponent>
  ): void {
    this.appRef.detachView(componentRef.hostView);
    componentRef.destroy();
  }
}
