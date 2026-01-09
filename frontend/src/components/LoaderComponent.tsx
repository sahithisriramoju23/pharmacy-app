type LoaderComponentProps = {
    loadingMessage?: string;
}
const LoaderIcon = () => (
  <svg className="w-8 h-8 animate-spin text-blue-600 mx-auto mb-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0 18c-4.42 0-8-3.58-8-8s3.58-8 8-8 8 3.58 8 8-3.58 8-8 8z" />
  </svg>
);

export const LoaderComponent = ({ loadingMessage }: LoaderComponentProps) => {
    return (
      <div className="flex items-center justify-center min-h-[400px]">
        <div className="text-center">
          <LoaderIcon />
          <p className="text-gray-600">{loadingMessage || "Loading..."}</p>
        </div>
      </div>
    );
}